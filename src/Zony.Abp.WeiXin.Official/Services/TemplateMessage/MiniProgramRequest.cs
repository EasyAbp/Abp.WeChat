using Newtonsoft.Json;

namespace Zony.Abp.WeiXin.Official.Services.TemplateMessage
{
    public class MiniProgramRequest
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("pagepath")]
        public string PagePath { get; set; }
    }
}