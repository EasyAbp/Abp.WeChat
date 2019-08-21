using Newtonsoft.Json;
using Zony.Abp.WeiXin.Official.Infrastructure.Models;

namespace Zony.Abp.WeiXin.Official.Services.TemplateMessage
{
    public class SendMessageResponse : IOfficialResponse
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }

        [JsonProperty("msgid")]
        public long MessageId { get; set; }
    }
}