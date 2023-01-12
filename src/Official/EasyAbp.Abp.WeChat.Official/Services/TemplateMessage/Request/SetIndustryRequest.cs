using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request
{
    /// <summary>
    /// 设置模版消息行业所需要的参数。
    /// </summary>
    public class SetIndustryRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 公众号模板消息所属行业编号。
        /// </summary>
        [JsonPropertyName("industry_id1")]
        [JsonProperty("industry_id1")]
        public string IndustryId1 { get; set; }

        /// <summary>
        /// 公众号模板消息所属行业编号。
        /// </summary>
        [JsonPropertyName("industry_id2")]
        [JsonProperty("industry_id2")]
        public string IndustryId2 { get; set; }

        protected SetIndustryRequest()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="SetIndustryRequest"/> 实例。
        /// </summary>
        /// <param name="industryId1">公众号模板消息所属行业编号。</param>
        /// <param name="industryId2">公众号模板消息所属行业编号。</param>
        public SetIndustryRequest(string industryId1, string industryId2)
        {
            IndustryId1 = industryId1;
            IndustryId2 = industryId2;
        }
    }
}