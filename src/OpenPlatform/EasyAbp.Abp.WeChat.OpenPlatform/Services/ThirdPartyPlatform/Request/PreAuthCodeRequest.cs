using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Request;

public class PreAuthCodeRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }
}