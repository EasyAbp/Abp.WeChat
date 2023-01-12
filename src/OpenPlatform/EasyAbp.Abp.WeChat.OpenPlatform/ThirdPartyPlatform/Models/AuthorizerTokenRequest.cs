using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class AuthorizerTokenRequest : OpenPlatformCommonRequest
{
    [JsonPropertyName("component_appid")]
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonPropertyName("authorizer_appid")]
    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }

    [JsonPropertyName("authorizer_refresh_token")]
    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }
}