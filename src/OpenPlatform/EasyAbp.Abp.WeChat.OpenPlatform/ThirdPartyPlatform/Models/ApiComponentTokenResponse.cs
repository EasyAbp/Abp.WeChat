using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class ApiComponentTokenResponse : OpenPlatformCommonResponse
{
    [JsonProperty("component_access_token")]
    public string ComponentAccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}