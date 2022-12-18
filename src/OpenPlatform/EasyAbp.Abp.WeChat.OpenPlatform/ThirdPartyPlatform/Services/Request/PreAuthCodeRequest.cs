using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Request;

public class PreAuthCodeRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }
}