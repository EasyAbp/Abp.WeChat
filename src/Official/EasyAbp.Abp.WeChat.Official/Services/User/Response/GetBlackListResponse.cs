using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetBlackListResponse : OfficialCommonResponse
    {
        [JsonProperty("total")] public int Total { get; protected set; }

        [JsonProperty("count")] public int Count { get; protected set; }

        [JsonProperty("data")] public BlackListData Data { get; protected set; }
    }

    public class BlackListData
    {
        [JsonProperty("openid")] public List<string> OpenIds { get; protected set; }
    }
}