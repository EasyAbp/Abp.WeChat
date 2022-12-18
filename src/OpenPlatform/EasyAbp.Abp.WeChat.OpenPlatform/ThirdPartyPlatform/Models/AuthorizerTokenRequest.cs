using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class AuthorizerTokenRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }

    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }
}