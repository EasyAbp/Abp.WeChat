using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;

public interface IOpenPlatformResponse
{
    [JsonPropertyName("errmsg")]
    [JsonProperty("errmsg")]
    string ErrorMessage { get; set; }

    [JsonPropertyName("errcode")]
    [JsonProperty("errcode")]
    int ErrorCode { get; set; }
}