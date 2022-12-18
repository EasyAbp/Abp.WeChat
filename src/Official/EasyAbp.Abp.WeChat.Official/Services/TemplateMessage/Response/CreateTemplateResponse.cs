using EasyAbp.Abp.WeChat.Official.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response
{
    /// <summary>
    /// 获得模版的完整唯一 ID 的响应参数。
    /// </summary>
    public class CreateTemplateResponse : OfficialCommonResponse
    {
        /// <summary>
        /// 模版的完整唯一 Id。
        /// </summary>
        public string TemplateId { get; set; }
    }
}