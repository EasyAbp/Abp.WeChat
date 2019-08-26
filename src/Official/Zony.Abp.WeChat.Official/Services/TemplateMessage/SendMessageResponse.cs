using Newtonsoft.Json;
using Zony.Abp.WeChat.Official.Infrastructure.Models;

namespace Zony.Abp.WeChat.Official.Services.TemplateMessage
{
    public class SendMessageResponse : IOfficialResponse
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }

        [JsonProperty("msgid")]
        public long MessageId { get; set; }
    }
}