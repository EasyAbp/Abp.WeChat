using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class AuthorizerTokenResponse : OpenPlatformCommonResponse
{
    [JsonProperty("authorizer_access_token")]
    public string AuthorizerAccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }
}