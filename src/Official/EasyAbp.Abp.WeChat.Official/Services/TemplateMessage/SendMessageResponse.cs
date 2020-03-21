using Newtonsoft.Json;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 定义了执行发送模板消息之后的请求响应。
    /// </summary>
    public class SendMessageResponse : IOfficialResponse
    {
        /// <summary>
        /// 微信公众号接口返回的错误信息。
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 微信公众号接口返回的错误代码。
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 发送消息成功之后的消息 Id。
        /// </summary>
        [JsonProperty("msgid")]
        public long MessageId { get; set; }
    }
}