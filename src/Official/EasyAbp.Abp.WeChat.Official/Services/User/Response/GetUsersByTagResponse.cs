using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetUsersByTagResponse : OfficialCommonResponse
    {
        [JsonPropertyName("count")]
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public GetUserByTagInnerData Data { get; set; }
        
        [JsonPropertyName("next_openid")]
        [JsonProperty("next_openid")]
        public string FirstOpenId { get; set; }
    }

    public class GetUserByTagInnerData
    {
        [JsonPropertyName("openid")]
        [JsonProperty("openid")] 
        public List<string> OpenIds { get; set; }

        [JsonPropertyName("next_openid")]
        [JsonProperty("next_openid")]
        public string FirstOpenId { get; set; }
    }
}