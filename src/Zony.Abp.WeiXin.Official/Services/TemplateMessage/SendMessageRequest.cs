using Newtonsoft.Json;
using Zony.Abp.WeiXin.Official.Infrastructure.Models;
using Zony.Abp.WeiXin.Official.Services.RequestDtos;
using Zony.Abp.WeiXin.Official.Services.TemplateMessage.Json;

namespace Zony.Abp.WeiXin.Official.Services.TemplateMessage
{
    public class SendMessageRequest : OfficialCommonRequest
    {
        [JsonProperty("touser")]
        public string ToUser { get; protected set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; protected set; }

        [JsonProperty("miniprogram")]
        public MiniProgramRequest MiniProgramRequest { get; protected set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("data")]
        public TemplateMessage TemplateMessage { get; protected set; }

        protected SendMessageRequest()
        {
            
        }

        public SendMessageRequest(string openId,
            string templateId,
            string targetUrl,
            TemplateMessage templateMessage,
            MiniProgramRequest miniProgramRequest = null)
        {
            ToUser = openId;
            TemplateId = templateId;
            MiniProgramRequest = miniProgramRequest;
            Url = targetUrl;
            TemplateMessage = templateMessage;
        }
    }
}