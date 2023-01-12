using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    public class Code2AccessTokenResponse : IOfficialResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("access_token")]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("scope")]
        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("openid")]
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonPropertyName("refresh_token")]
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}