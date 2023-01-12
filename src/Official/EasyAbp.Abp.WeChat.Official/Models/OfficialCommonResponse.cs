using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Models
{
    public class OfficialCommonResponse : IOfficialResponse
    {
        [JsonPropertyName("errmsg")]
        [JsonProperty("errmsg")] 
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        [JsonProperty("errcode")] 
        public int ErrorCode { get; set; }
    }
}