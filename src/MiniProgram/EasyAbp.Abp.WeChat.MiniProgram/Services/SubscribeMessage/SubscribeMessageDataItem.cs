using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage
{
    public class SubscribeMessageDataItem
    {
        [JsonPropertyName("value")]
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}