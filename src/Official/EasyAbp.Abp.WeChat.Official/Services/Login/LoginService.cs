using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    /// <summary>
    /// 公众号登录服务。
    /// </summary>
    public class LoginService : CommonService
    {
        private readonly IWeChatOfficialOptionsResolver _optionsResolver;

        public LoginService(IWeChatOfficialOptionsResolver optionsResolver)
        {
            _optionsResolver = optionsResolver;
        }
        
        /// <summary>
        /// 登录凭证校验。通过 wx.login 接口获得临时登录凭证 code 后传到开发者服务器调用此接口完成登录流程。
        /// </summary>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public virtual async Task<Code2AccessTokenResponse> Code2SessionAsync(string code, string grantType = "authorization_code")
        {
            var options = await _optionsResolver.ResolveAsync();

            return await Code2SessionAsync(options.AppId, options.AppSecret, code, grantType);
        }
        
        /// <summary>
        /// 登录凭证校验。通过 wx.login 接口获得临时登录凭证 code 后传到开发者服务器调用此接口完成登录流程。
        /// </summary>
        /// <param name="appId">公众号 appId</param>
        /// <param name="appSecret">公众号 appSecret</param>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public Task<Code2AccessTokenResponse> Code2SessionAsync(string appId, string appSecret, string code, string grantType = "authorization_code")
        {
            const string targetUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?";

            var request = new Code2AccessTokenRequest(appId, appSecret, code, grantType);

            return WeChatOfficialApiRequester.RequestAsync<Code2AccessTokenResponse>(targetUrl, HttpMethod.Get, request, false);
        }
    }
}