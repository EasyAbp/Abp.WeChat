using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

public class ApiComponentTokenResponse : OpenPlatformCommonResponse
{
    [JsonProperty("component_access_token")]
    public string ComponentAccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}