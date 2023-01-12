using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class AuthorizerTokenResponse : OpenPlatformCommonResponse
{
    [JsonPropertyName("authorizer_access_token")]
    [JsonProperty("authorizer_access_token")]
    public string AuthorizerAccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("authorizer_refresh_token")]
    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }
}