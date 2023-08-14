using System.Text.Json.Serialization;
using System.Collections.Generic;
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
        /// 模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式,对于类目模板，为纯数字ID
        /// </summary>
        [JsonPropertyName("template_id_short")]
        [JsonProperty("template_id_short")]
        public string TemplateShortId { get; protected set; }

        /// <summary>
        /// 选用的类目模板的关键词,按顺序传入,如果为空，或者关键词不在模板库中，会返回40246错误码
        /// </summary>
        [JsonProperty("keyword_name_list")]
        public List<string> KeywordNameList { get; protected set; }

        protected CreateTemplateRequest()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="CreateTemplateRequest"/> 对象。
        /// </summary>
        /// <param name="templateShortId">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式,对于类目模板，为纯数字ID</param>
        /// <param name="keywordNameList">选用的类目模板的关键词,按顺序传入,如果为空，或者关键词不在模板库中，会返回40246错误码</param>
        public CreateTemplateRequest(string templateShortId, List<string> keywordNameList)
        {
            TemplateShortId = templateShortId;
            KeywordNameList = keywordNameList;
        }
    }
}