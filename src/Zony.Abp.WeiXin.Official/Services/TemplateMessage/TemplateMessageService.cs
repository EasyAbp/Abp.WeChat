using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zony.Abp.WeiXin.Official.Services.TemplateMessage
{
    public class TemplateMessageService : CommonService
    {
        protected const string TargetUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";

        /// <summary>
        /// 发送模板消息。
        /// </summary>
        public Task<SendMessageResponse> SendMessageAsync(string openId,string templateId,string targetUrl,TemplateMessage templateMessage,MiniProgramRequest miniProgramRequest = null)
        {
            var request = new SendMessageRequest(openId,
                templateId,
                targetUrl,
                templateMessage,
                miniProgramRequest);

            return WeiXinOfficialApiRequester.RequestAsync<SendMessageResponse>(TargetUrl, HttpMethod.Post, request);
        }
        
        /// <summary>
        /// 发送模板消息。
        /// </summary>
        public Task<SendMessageResponse> SendMessageAsync(string openId,string templateId,string targetUrl,string templateMessage,MiniProgramRequest miniProgramRequest = null)
        {
            return SendMessageAsync(openId, templateId, targetUrl, JsonConvert.DeserializeObject<TemplateMessage>(templateMessage), miniProgramRequest);
        }
    }
}