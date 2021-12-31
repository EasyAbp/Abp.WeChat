using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class OfficialUserListResponse : OfficialCommonResponse
    {
        [JsonProperty("total")] public int Total { get; protected set; }

        [JsonProperty("count")] public int Count { get; protected set; }

        [JsonProperty("data")] public OfficialOpenIdsData Data { get; protected set; }

        [JsonProperty("next_openid")] public string NextOpenId { get; protected set; }
    }

    public class OfficialOpenIdsData
    {
        [JsonProperty("openid")] public List<string> OpenIds { get; protected set; }
    }
}