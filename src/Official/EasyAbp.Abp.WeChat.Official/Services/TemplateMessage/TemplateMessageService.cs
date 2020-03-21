using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 模板消息服务，可以注入本服务来发送微信模板消息。
    /// </summary>
    public class TemplateMessageService : CommonService
    {
        protected const string TargetUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";

        /// <summary>
        /// 请求微信公众号的 API 发送指定的模板消息。
        /// </summary>
        /// <param name="openId">目标微信用户的 OpenId。</param>
        /// <param name="templateId">需要发送的模板消息 Id。</param>
        /// <param name="targetUrl">微信用户收到模板消息时，点击之后需要跳转的 Url。</param>
        /// <param name="templateMessage">需要发送的模板消息内容。</param>
        /// <param name="miniProgramRequest">模板关联的小程序参数，如果没有的话可以不用传递。</param>
        public Task<SendMessageResponse> SendMessageAsync(string openId,string templateId,string targetUrl,TemplateMessage templateMessage,MiniProgramRequest miniProgramRequest = null)
        {
            var request = new SendMessageRequest(openId,
                templateId,
                targetUrl,
                templateMessage,
                miniProgramRequest);

            return WeChatOfficialApiRequester.RequestAsync<SendMessageResponse>(TargetUrl, HttpMethod.Post, request);
        }
        
        /// <summary>
        /// 请求微信公众号的 API 发送指定的模板消息。
        /// </summary>
        /// <param name="openId">目标微信用户的 OpenId。</param>
        /// <param name="templateId">需要发送的模板消息 Id。</param>
        /// <param name="targetUrl">微信用户收到模板消息时，点击之后需要跳转的 Url。</param>
        /// <param name="templateMessage">需要发送的模板消息内容。，这里的模板消息可以是用户提前存储的 JSON 字串。</param>
        /// <param name="miniProgramRequest">模板关联的小程序参数，如果没有的话可以不用传递。</param>
        public Task<SendMessageResponse> SendMessageAsync(string openId,string templateId,string targetUrl,string templateMessage,MiniProgramRequest miniProgramRequest = null)
        {
            return SendMessageAsync(openId, templateId, targetUrl, JsonConvert.DeserializeObject<TemplateMessage>(templateMessage), miniProgramRequest);
        }
    }
}