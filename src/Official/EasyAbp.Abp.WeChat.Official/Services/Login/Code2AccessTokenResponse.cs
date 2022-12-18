using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    public class Code2AccessTokenResponse : IOfficialResponse
    {
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}