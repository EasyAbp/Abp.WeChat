using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Models
{
    public interface IMiniProgramResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")]
        string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")]
        int ErrorCode { get; set; }
    }
}