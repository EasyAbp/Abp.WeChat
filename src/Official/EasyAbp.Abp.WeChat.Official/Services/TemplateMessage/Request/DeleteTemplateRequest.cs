using EasyAbp.Abp.WeChat.Official.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request
{
    /// <summary>
    /// 根据模版 Id 删除指定的模版所需要的请求参数。
    /// </summary>
    public class DeleteTemplateRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 公众帐号下模板消息 ID。
        /// </summary>
        public string TemplateId { get; protected set; }

        protected DeleteTemplateRequest()
        {
            
        }

        /// <summary>
        /// 构建一个新的 <see cref="DeleteTemplateRequest"/> 对象。
        /// </summary>
        /// <param name="templateId">公众帐号下模板消息 ID。</param>
        public DeleteTemplateRequest(string templateId)
        {
            TemplateId = templateId;
        }
    }
}