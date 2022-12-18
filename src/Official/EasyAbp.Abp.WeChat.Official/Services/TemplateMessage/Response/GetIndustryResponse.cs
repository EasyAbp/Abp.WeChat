using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response
{
    /// <summary>
    /// 获取消息模版对应的行业信息。
    /// </summary>
    public class GetIndustryResponse : OfficialCommonResponse
    {
        /// <summary>
        /// 账号设置的主营行业。
        /// </summary>
        [JsonProperty("primary_industry")]
        public IndustryItem PrimaryIndustry { get; set; }

        /// <summary>
        /// 账号设置的副营行业。
        /// </summary>
        [JsonProperty("secondary_industry")]
        public IndustryItem SecondaryIndustry { get; set; }
    }
    
    public class IndustryItem
    {
        /// <summary>
        /// 主行业。
        /// </summary>
        [JsonProperty("first_class")]
        public string FirstClass { get; set; }

        /// <summary>
        /// 副行业。
        /// </summary>
        [JsonProperty("second_class")]
        public string SecondClass { get; set; }
    }
}