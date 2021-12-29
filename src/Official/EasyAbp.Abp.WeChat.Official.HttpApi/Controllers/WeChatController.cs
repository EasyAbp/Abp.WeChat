using System.Net.Http;
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
using EasyAbp.Abp.WeChat.Official.Services.Login;

namespace EasyAbp.Abp.WeChat.Official.HttpApi.Controllers
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChat")]
    [Route("/wechat")]
    public class WeChatController : AbpControllerBase
    {
        private readonly IWeChatOfficialOptionsResolver _optionsResolver;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly SignatureChecker _signatureChecker;
        private readonly ISignatureGenerator _signatureGenerator;

        private readonly IJsTicketAccessor _jsTicketAccessor;

        private readonly LoginService _loginService;

        public WeChatController(SignatureChecker signatureChecker,
            IHttpClientFactory httpClientFactory,
            IJsTicketAccessor jsTicketAccessor,
            ISignatureGenerator signatureGenerator,
            LoginService loginService,
            IWeChatOfficialOptionsResolver optionsResolver)
        {
            _signatureChecker = signatureChecker;
            _httpClientFactory = httpClientFactory;
            _jsTicketAccessor = jsTicketAccessor;
            _signatureGenerator = signatureGenerator;
            _optionsResolver = optionsResolver;
            _loginService = loginService;
        }

        [HttpGet]
        [Route("verify")]
        public virtual async Task<string> Verify(VerifyRequestDto input)
        {
            var options = await _optionsResolver.ResolveAsync();
            if (_signatureChecker.Validate(options.Token, input.Timestamp, input.Nonce, input.Signature))
            {
                return input.EchoStr;
            }

            return "非法参数。";
        }

        [HttpGet]
        [Route("redirect-url")]
        public virtual async Task<ActionResult> RedirectUrl(RedirectUrlRequest input)
        {
            if (input == null) return BadRequest();
            if (string.IsNullOrEmpty(input.Code)) return BadRequest();

            var options = await _optionsResolver.ResolveAsync();
            return Redirect($"{options.OAuthRedirectUrl.TrimEnd('/')}?code={input.Code}");
        }

        [HttpGet]
        [Route("access-token-by-code")]
        public virtual async Task<Code2AccessTokenResponse> GetAccessTokenByCode([FromQuery] string code)
        {
            return await _loginService.Code2SessionAsync(code);
        }

        [HttpGet]
        [Route("js-sdk-config-parameters")]
        public virtual async Task<ActionResult> GetJsSdkConfigParameters([FromQuery] string jsUrl)
        {
            if (string.IsNullOrEmpty(jsUrl)) throw new UserFriendlyException("需要计算的 JS URL 参数不能够为空。");

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
                appid = (await _optionsResolver.ResolveAsync()).AppId,
                noncestr = nonceStr,
                timestamp = timeStamp,
                signature = signStr,
                jsapi_ticket = ticket
            });
        }
    }
}