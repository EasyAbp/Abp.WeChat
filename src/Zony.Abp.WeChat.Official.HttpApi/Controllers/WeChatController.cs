using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Zony.Abp.WeChat.Common;
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
        private readonly SignatureChecker _signatureChecker;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeChatController(SignatureChecker signatureChecker,
            IOptions<AbpWeChatOfficialOptions> officialOptions,
            IHttpClientFactory httpClientFactory)
        {
            _signatureChecker = signatureChecker;
            _httpClientFactory = httpClientFactory;
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
        public virtual async Task<ActionResult> GetAccessTokenByCode([FromQuery]string code)
        {
            var client = _httpClientFactory.CreateClient();
            var requestUrl = $@"https://api.weixin.qq.com/sns/oauth2/access_token?grant_type={GrantTypes.AuthorizationCode}&appid={_officialOptions.AppId}&secret={_officialOptions.AppSecret}&code={code}";

            var resultStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                .Content.ReadAsStringAsync();
            
            return Content(resultStr,"application/json; encoding=utf-8");
        }
    }
}