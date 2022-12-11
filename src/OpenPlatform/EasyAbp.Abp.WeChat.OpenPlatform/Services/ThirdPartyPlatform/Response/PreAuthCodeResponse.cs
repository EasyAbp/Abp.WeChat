using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;

public class PreAuthCodeResponse : OpenPlatformCommonResponse
{
    [JsonProperty("pre_auth_code")]
    public string PreAuthCode { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}