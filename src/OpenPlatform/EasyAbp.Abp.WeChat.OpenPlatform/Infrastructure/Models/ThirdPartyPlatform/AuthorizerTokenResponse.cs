using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

public class AuthorizerTokenResponse : OpenPlatformCommonResponse
{
    [JsonProperty("authorizer_access_token")]
    public string AuthorizerAccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }
}