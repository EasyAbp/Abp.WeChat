using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response
{
    public class GetAllPrivateTemplateResponse : OfficialCommonResponse
    {
        /// <summary>
        /// 模版列表数据。
        /// </summary>
        [JsonPropertyName("template_list")]
        [JsonProperty("template_list")]
        public List<TemplateDefinition> TemplateList { get; set; }
    }
}