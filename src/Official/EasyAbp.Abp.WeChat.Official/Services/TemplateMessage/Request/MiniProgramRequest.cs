using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request
{
    public class MiniProgramRequest
    {
        [JsonPropertyName("appid")]
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonPropertyName("pagepath")]
        [JsonProperty("pagepath")]
        public string PagePath { get; set; }
    }
}