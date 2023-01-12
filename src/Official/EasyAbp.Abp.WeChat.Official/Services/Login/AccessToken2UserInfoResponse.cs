using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    public class AccessToken2UserInfoResponse : IOfficialResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("openid")]
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonPropertyName("unionid")]
        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        [JsonPropertyName("nickname")]
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonPropertyName("sex")]
        [JsonProperty("sex")]
        public byte Gender { get; set; }

        [JsonPropertyName("city")]
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonPropertyName("province")]
        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonPropertyName("country")]
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonPropertyName("headimgurl")]
        [JsonProperty("headimgurl")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("privilege")]
        [JsonProperty("privilege")]
        public List<string> Privilege { get; set; }
    }
}
