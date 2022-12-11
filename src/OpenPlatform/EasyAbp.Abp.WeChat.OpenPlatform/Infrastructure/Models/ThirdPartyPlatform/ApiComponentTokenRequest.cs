using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

public class ApiComponentTokenRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonProperty("component_appsecret")]
    public string ComponentAppSecret { get; set; }

    [JsonProperty("component_verify_ticket")]
    public string ComponentVerifyTicket { get; set; }
}