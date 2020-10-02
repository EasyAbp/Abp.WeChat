using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage
{
    public class SubscribeMessageDataItem
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}