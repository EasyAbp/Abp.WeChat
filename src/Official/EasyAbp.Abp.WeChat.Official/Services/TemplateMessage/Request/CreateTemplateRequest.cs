using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request
{
    /// <summary>
    /// 获得模版的完整唯一 ID 的请求参数。
    /// </summary>
    public class CreateTemplateRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 模板库中模板的编号，有 "TM**" 和 "OPENTMTM**" 等形式。
        /// </summary>
        [JsonProperty("template_id_short")]
        public string TemplateShortId { get; protected set; }

        protected CreateTemplateRequest()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="CreateTemplateRequest"/> 对象。
        /// </summary>
        /// <param name="templateShortId">模板库中模板的编号，有 "TM**" 和 "OPENTMTM**" 等形式。</param>
        public CreateTemplateRequest(string templateShortId)
        {
        }
    }
}