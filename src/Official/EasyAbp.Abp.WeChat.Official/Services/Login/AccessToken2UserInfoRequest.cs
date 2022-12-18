using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    /// <summary>
    /// 拉取用户信息(需 scope 为 snsapi_userinfo)时，需要传递的请求参数。
    /// </summary>
    public class AccessToken2UserInfoRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; protected set; }

        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; protected set; }

        protected AccessToken2UserInfoRequest()
        {

        }

        /// <summary>
        /// 构造一个新的 <see cref="Code2AccessTokenRequest"/> 实例。
        /// </summary>
        /// <param name="appId">公众号 appId</param>
        /// <param name="appSecret">公众号 appSecret</param>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public AccessToken2UserInfoRequest(string accessToken, string openId, string language)
        {
            AccessToken = accessToken;
            OpenId = openId;
            Language = language;
        }
    }
}
