using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.Login
{
    /// <summary>
    /// 登录时，需要传递的请求参数。
    /// </summary>
    public class Code2SessionRequest : MiniProgramCommonRequest
    {
        /// <summary>
        /// 小程序 appId
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; protected set; }

        /// <summary>
        /// 小程序 appSecret
        /// </summary>
        [JsonProperty("secret")]
        public string AppSecret { get; protected set; }

        /// <summary>
        /// 登录时获取的 code
        /// </summary>
        [JsonProperty("js_code")]
        public string JsCode { get; protected set; }
        
        /// <summary>
        /// 授权类型，此处只需填写 authorization_code
        /// </summary>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        
        protected Code2SessionRequest()
        {
            
        }

        /// <summary>
        /// 构造一个新的 <see cref="Code2SessionRequest"/> 实例。
        /// </summary>
        /// <param name="appId">小程序 appId</param>
        /// <param name="appSecret">小程序 appSecret</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public Code2SessionRequest(string appId, string appSecret, string jsCode, string grantType)
        {
            AppId = appId;
            AppSecret = appSecret;
            JsCode = jsCode;
            GrantType = grantType;
        }
    }
}