using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;

public class PreAuthCodeResponse : OpenPlatformCommonResponse
{
    [JsonProperty("pre_auth_code")]
    public string PreAuthCode { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}