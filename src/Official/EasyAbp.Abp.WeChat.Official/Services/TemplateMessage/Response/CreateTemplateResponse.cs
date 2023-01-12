using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

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
        [JsonPropertyName("template_id")]
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }
    }
}