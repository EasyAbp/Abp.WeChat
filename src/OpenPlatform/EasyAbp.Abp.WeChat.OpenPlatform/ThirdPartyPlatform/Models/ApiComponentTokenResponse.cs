using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class ApiComponentTokenResponse : OpenPlatformCommonResponse
{
    [JsonPropertyName("component_access_token")]
    [JsonProperty("component_access_token")]
    public string ComponentAccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}