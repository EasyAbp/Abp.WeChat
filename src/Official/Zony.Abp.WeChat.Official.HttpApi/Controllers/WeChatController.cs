using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Zony.Abp.WeChat.Common;
using Zony.Abp.WeChat.Common.Extensions;
using Zony.Abp.WeChat.Common.Infrastructure;
using Zony.Abp.WeChat.Common.Infrastructure.Signature;
using Zony.Abp.WeChat.Official.HttpApi.Models;
using Zony.Abp.WeChat.Official.Infrastructure;

namespace Zony.Abp.WeChat.Official.HttpApi.Controllers
{
    [RemoteService]
    [ControllerName("WeChat")]
    [Route("/WeChat")]
    public class WeChatController : AbpController
    {
        private readonly AbpWeChatOfficialOptions _officialOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly SignatureChecker _signatureChecker;
        private readonly ISignatureGenerator _signatureGenerator;
        
        private readonly IJsTicketAccessor _jsTicketAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeChatController(SignatureChecker signatureChecker,
            IOptions<AbpWeChatOfficialOptions> officialOptions,
            IHttpClientFactory httpClientFactory,
            IJsTicketAccessor jsTicketAccessor,
            ISignatureGenerator signatureGenerator,
            IHttpContextAccessor httpContextAccessor)
        {
            _signatureChecker = signatureChecker;
            _httpClientFactory = httpClientFactory;
            _jsTicketAccessor = jsTicketAccessor;
            _signatureGenerator = signatureGenerator;
            _httpContextAccessor = httpContextAccessor;
            _officialOptions = officialOptions.Value;
        }

        [HttpGet]
        [Route("Verify")]
        public virtual Task<string> Verify(VerifyRequestDto input)
        {
            if (_signatureChecker.Validate(_officialOptions.Token, input.Timestamp, input.Nonce, input.Signature))
            {
                return Task.FromResult(input.EchoStr);
            }

            return Task.FromResult("非法参数。");
        }

        [HttpGet]
        [Route("RedirectUrl")]
        public virtual ActionResult RedirectUrl(RedirectUrlRequest input)
        {
            if (input == null) return BadRequest();
            if (string.IsNullOrEmpty(input.Code)) return BadRequest();

            return Redirect($"{_officialOptions.OAuthRedirectUrl.TrimEnd('/')}?code={input.Code}");
        }

        [HttpGet]
        [Route("GetAccessTokenByCode")]
        public virtual async Task<ActionResult> GetAccessTokenByCode([FromQuery] string code)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUrl = $@"https://api.weixin.qq.com/sns/oauth2/access_token?grant_type={GrantTypes.AuthorizationCode}&appid={_officialOptions.AppId}&secret={_officialOptions.AppSecret}&code={code}";

            var resultStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                .Content.ReadAsStringAsync();

            return Content(resultStr, "application/json; encoding=utf-8");
        }

        [HttpGet]
        [Route("GetJsSdkConfigParameters")]
        public virtual async Task<ActionResult> GetJsSdkConfigParameters([FromQuery] string jsUrl)
        {
            if(string.IsNullOrEmpty(jsUrl)) throw new UserFriendlyException("需要计算的 JS URL 参数不能够为空。");
            
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
                appid = _officialOptions.AppId,
                noncestr = nonceStr,
                timestamp = timeStamp,
                signature = signStr,
                jsapi_ticket = ticket
            });
        }
    }
}