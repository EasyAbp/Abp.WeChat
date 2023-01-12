using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class ApiComponentTokenRequest : OpenPlatformCommonRequest
{
    [JsonPropertyName("component_appid")]
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonPropertyName("component_appsecret")]
    [JsonProperty("component_appsecret")]
    public string ComponentAppSecret { get; set; }

    [JsonPropertyName("component_verify_ticket")]
    [JsonProperty("component_verify_ticket")]
    public string ComponentVerifyTicket { get; set; }
}