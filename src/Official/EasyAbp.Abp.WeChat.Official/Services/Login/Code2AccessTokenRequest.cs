using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    /// <summary>
    /// 通过 code 换取网页授权access_token时，需要传递的请求参数。
    /// </summary>
    public class Code2AccessTokenRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 公众号 appId
        /// </summary>
        [JsonPropertyName("appid")]
        [JsonProperty("appid")]
        public string AppId { get; protected set; }

        /// <summary>
        /// 公众号 appSecret
        /// </summary>
        [JsonPropertyName("secret")]
        [JsonProperty("secret")]
        public string AppSecret { get; protected set; }

        /// <summary>
        /// 登录时获取的 code
        /// </summary>
        [JsonPropertyName("code")]
        [JsonProperty("code")]
        public string Code { get; protected set; }
        
        /// <summary>
        /// 授权类型，此处只需填写 authorization_code
        /// </summary>
        [JsonPropertyName("grant_type")]
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        
        protected Code2AccessTokenRequest()
        {
            
        }

        /// <summary>
        /// 构造一个新的 <see cref="Code2AccessTokenRequest"/> 实例。
        /// </summary>
        /// <param name="appId">公众号 appId</param>
        /// <param name="appSecret">公众号 appSecret</param>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public Code2AccessTokenRequest(string appId, string appSecret, string code, string grantType)
        {
            AppId = appId;
            AppSecret = appSecret;
            Code = code;
            GrantType = grantType;
        }
    }
}