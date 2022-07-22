using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyAbp.Abp.WeChat.Official.Services.Login
{
    public class AccessToken2UserInfoResponse : IOfficialResponse
    {
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("sex")]
        public byte Gender { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("headimgurl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("privilege")]
        public List<string> Privilege { get; set; }
    }
}
