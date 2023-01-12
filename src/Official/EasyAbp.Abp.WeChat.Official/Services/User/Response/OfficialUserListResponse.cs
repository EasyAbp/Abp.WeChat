using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class OfficialUserListResponse : OfficialCommonResponse
    {
        [JsonPropertyName("total")]
        [JsonProperty("total")]
        public int Total { get; protected set; }

        [JsonPropertyName("count")]
        [JsonProperty("count")]
        public int Count { get; protected set; }

        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public OfficialOpenIdsData Data { get; protected set; }

        [JsonPropertyName("next_openid")]
        [JsonProperty("next_openid")]
        public string NextOpenId { get; protected set; }
    }

    public class OfficialOpenIdsData
    {
        [JsonPropertyName("openid")]
        [JsonProperty("openid")]
        public List<string> OpenIds { get; protected set; }
    }
}