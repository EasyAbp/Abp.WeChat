using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using EasyAbp.Abp.WeChat.Common;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Official.HttpApi.Models;
using EasyAbp.Abp.WeChat.Official.Infrastructure;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.HttpApi.Controllers
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChat")]
    [Route("/wechat")]
    public class WeChatController : AbpControllerBase
    {
        private readonly SignatureChecker _signatureChecker;
        private readonly IJsTicketAccessor _jsTicketAccessor;
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly LoginService _loginService;
        private readonly IWeChatOfficialAsyncLocal _weChatOfficialAsyncLocal;
        private readonly IWeChatOfficialOptionsResolver _optionsResolver;

        public WeChatController(SignatureChecker signatureChecker,
            IJsTicketAccessor jsTicketAccessor,
            ISignatureGenerator signatureGenerator,
            LoginService loginService,
            IWeChatOfficialAsyncLocal weChatOfficialAsyncLocal,
            IWeChatOfficialOptionsResolver optionsResolver)
        {
            _signatureChecker = signatureChecker;
            _jsTicketAccessor = jsTicketAccessor;
            _signatureGenerator = signatureGenerator;
            _optionsResolver = optionsResolver;
            _loginService = loginService;
            _weChatOfficialAsyncLocal = weChatOfficialAsyncLocal;
        }

        [HttpGet]
        [Route("verify")]
        [Route("verify/tenant-id/{tenantId}")]
        [Route("verify/app-id/{appId}")]
        [Route("verify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<string> Verify(
            VerifyRequestDto input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            if (_signatureChecker.Validate(options.Token, input.Timestamp, input.Nonce, input.Signature))
            {
                return input.EchoStr;
            }

            return "非法参数。";
        }

        [HttpGet]
        [Route("redirect-url")]
        [Route("redirect-url/tenant-id/{tenantId}")]
        [Route("redirect-url/app-id/{appId}")]
        [Route("redirect-url/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<ActionResult> RedirectUrl(RedirectUrlRequest input, [CanBeNull] string tenantId,
            [CanBeNull] string appId)
        {
            if (input == null) return BadRequest();
            if (string.IsNullOrEmpty(input.Code)) return BadRequest();

            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            return Redirect($"{options.OAuthRedirectUrl.TrimEnd('/')}?code={input.Code}");
        }

        [HttpGet]
        [Route("access-token-by-code")]
        [Route("access-token-by-code/tenant-id/{tenantId}")]
        [Route("access-token-by-code/app-id/{appId}")]
        [Route("access-token-by-code/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<Code2AccessTokenResponse> GetAccessTokenByCode([FromQuery] string code,
            [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            using var changeOptions = _weChatOfficialAsyncLocal.Change(options);

            return await _loginService.Code2AccessTokenAsync(code);
        }

        [HttpGet]
        [Route("js-sdk-config-parameters")]
        [Route("js-sdk-config-parameters/tenant-id/{tenantId}")]
        [Route("js-sdk-config-parameters/app-id/{appId}")]
        [Route("js-sdk-config-parameters/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<ActionResult> GetJsSdkConfigParameters([FromQuery] string jsUrl,
            [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            if (string.IsNullOrEmpty(jsUrl)) throw new UserFriendlyException("需要计算的 JS URL 参数不能够为空。");

            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            using var changeOptions = _weChatOfficialAsyncLocal.Change(options);

            var nonceStr = RandomStringHelper.GetRandomString();
            var timeStamp = DateTimeHelper.GetNowTimeStamp();
            var ticket = await _jsTicketAccessor.GetTicketAsync();

            var @params = new WeChatParameters();
            @params.AddParameter("noncestr", nonceStr);
            @params.AddParameter("jsapi_ticket", await _jsTicketAccessor.GetTicketAsync());
            @params.AddParameter("url", HttpUtility.UrlDecode(jsUrl));
            @params.AddParameter("timestamp", timeStamp);

            var signStr = _signatureGenerator.Generate(@params, SHA1.Create()).ToLower();

            return new JsonResult(new
            {
                appid = options.AppId,
                noncestr = nonceStr,
                timestamp = timeStamp,
                signature = signStr,
                jsapi_ticket = ticket
            });
        }

        protected virtual async Task<IWeChatOfficialOptions> ResolveOptionsAsync(string appId)
        {
            var provider = LazyServiceProvider.LazyGetRequiredService<IHttpApiWeChatOfficialOptionsProvider>();
            return appId.IsNullOrWhiteSpace() ? await _optionsResolver.ResolveAsync() : await provider.GetAsync(appId);
        }
    }
}