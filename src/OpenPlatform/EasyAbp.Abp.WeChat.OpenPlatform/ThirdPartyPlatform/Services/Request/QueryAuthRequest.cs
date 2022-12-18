using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Request;

public class QueryAuthRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonProperty("authorization_code")]
    public string AuthorizationCode { get; set; }
}