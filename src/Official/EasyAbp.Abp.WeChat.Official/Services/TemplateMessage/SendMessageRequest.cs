using Newtonsoft.Json;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 发送模板消息时，需要传递的请求参数。
    /// </summary>
    public class SendMessageRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 模板消息的目标用户，一般为微信用户的 OpenId 值。
        /// </summary>
        [JsonProperty("touser")]
        public string ToUser { get; protected set; }

        /// <summary>
        /// 需要发送的模板消息 Id。
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; protected set; }

        /// <summary>
        /// 模板关联的小程序参数。
        /// </summary>
        [JsonProperty("miniprogram")]
        public MiniProgramRequest MiniProgramRequest { get; protected set; }
        
        /// <summary>
        /// 微信用户收到模板消息时，点击之后需要跳转的 Url。
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        
        /// <summary>
        /// 需要发送的模板消息内容。
        /// </summary>
        [JsonProperty("data")]
        public TemplateMessage TemplateMessage { get; protected set; }

        protected SendMessageRequest()
        {
            
        }

        /// <summary>
        /// 构造一个新的 <see cref="SendMessageRequest"/> 实例。
        /// </summary>
        /// <param name="openId">目标微信用户的 OpenId。</param>
        /// <param name="templateId">需要发送的模板消息 Id。</param>
        /// <param name="targetUrl">微信用户收到模板消息时，点击之后需要跳转的 Url。</param>
        /// <param name="templateMessage">需要发送的模板消息内容。</param>
        /// <param name="miniProgramRequest">模板关联的小程序参数，如果没有的话可以不用传递。</param>
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