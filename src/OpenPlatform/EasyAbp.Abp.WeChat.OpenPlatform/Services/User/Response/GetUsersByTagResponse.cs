using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class GetUsersByTagResponse : OpenPlatformCommonResponse
{
    [JsonProperty("count")]
    public long Count { get; set; }

    [JsonProperty("data")]
    public GetUserByTagInnerData Data { get; set; }
        
    [JsonProperty("next_openid")]
    public string FirstOpenId { get; set; }
}

public class GetUserByTagInnerData
{
    [JsonProperty("openid")] 
    public List<string> OpenIds { get; set; }

    [JsonProperty("next_openid")]
    public string FirstOpenId { get; set; }
}