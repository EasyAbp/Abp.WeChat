using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Models
{
    public class MiniProgramCommonResponse : IMiniProgramResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }
    }
}