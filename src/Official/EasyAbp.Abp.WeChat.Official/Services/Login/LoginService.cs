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
        /// 登录凭证校验。通过 code 换取网页授权access_token。
        /// </summary>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public virtual async Task<Code2AccessTokenResponse> Code2AccessTokenAsync(string code, string grantType = "authorization_code")
        {
            var options = await _optionsResolver.ResolveAsync();

            return await Code2AccessTokenAsync(options.AppId, options.AppSecret, code, grantType);
        }

        /// <summary>
        /// 登录凭证校验。通过 code 换取网页授权access_token。
        /// </summary>
        /// <param name="appId">公众号 appId</param>
        /// <param name="appSecret">公众号 appSecret</param>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public virtual Task<Code2AccessTokenResponse> Code2AccessTokenAsync(string appId, string appSecret, string code, string grantType = "authorization_code")
        {
            const string targetUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?";

            var request = new Code2AccessTokenRequest(appId, appSecret, code, grantType);

            return WeChatOfficialApiRequester.RequestAsync<Code2AccessTokenResponse>(targetUrl, HttpMethod.Get, request, false);
        }

        /// <summary>
        /// 拉取用户信息(需 scope 为 snsapi_userinfo)
        /// </summary>
        /// <param name="accessToken">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <param name="language">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        public virtual Task<AccessToken2UserInfoResponse> AccessToken2UserInfoAsync(string accessToken, string openId, string language = "zh_CN")
        {
            const string targetUrl = "https://api.weixin.qq.com/sns/userinfo?";

            var request = new AccessToken2UserInfoRequest(accessToken, openId, language);

            return WeChatOfficialApiRequester.RequestAsync<AccessToken2UserInfoResponse>(targetUrl, HttpMethod.Get, request, false);
        }
    }
}