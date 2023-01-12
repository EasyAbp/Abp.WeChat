using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;

public class PreAuthCodeResponse : OpenPlatformCommonResponse
{
    [JsonPropertyName("pre_auth_code")]
    [JsonProperty("pre_auth_code")]
    public string PreAuthCode { get; set; }

    [JsonPropertyName("expires_in")]
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}