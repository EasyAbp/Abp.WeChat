namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 模版消息的模版定义。
    /// </summary>
    public class TemplateDefinition
    {
        /// <summary>
        /// 模版 Id。
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 模版标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 模版所属行业的一级行业。
        /// </summary>
        public string PrimaryIndustry { get; set; }

        /// <summary>
        /// 模版所属行业的二级行业。
        /// </summary>
        public string DeputyIndustry { get; set; }

        /// <summary>
        /// 模版内容。
        /// </summary>
        public string Content { get; set; }

        public string Example { get; set; }
    }
}