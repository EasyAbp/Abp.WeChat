using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Models
{
    public interface IOfficialResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")]
        string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")]
        int ErrorCode { get; set; }
    }
}