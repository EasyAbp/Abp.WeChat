using System.Collections.Generic;
using System.Drawing;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 模板消息的数据定义，开发人员可以构造模板消息进行发送。
    /// </summary>
    public class TemplateMessage : Dictionary<string,TemplateMessageItem>
    {
        /// <summary>
        /// 构造一个新的 <see cref="TemplateMessage"/> 实例，需要开发人员自行构造 first 和 remark 参数。
        /// </summary>
        public TemplateMessage()
        {
            
        }
        
        /// <summary>
        /// 构造一个新的 <see cref="TemplateMessage"/> 实例，其中文字颜色默认为黑色(<see cref="Color.Black"/>)。
        /// </summary>
        /// <param name="first">模板消息的开始内容。</param>
        /// <param name="remark">模板消息的结束内容。</param>
        public TemplateMessage(string first, string remark)
        {
            Add("first",new TemplateMessageItem(first));
            Add("remark",new TemplateMessageItem(remark));
        }
        
        /// <summary>
        /// 构建一个新的 <see cref="TemplateMessage"/> 实例。
        /// </summary>
        /// <param name="first">模板消息的开始内容。</param>
        /// <param name="remark">模板消息的结束内容。</param>
        public TemplateMessage(TemplateMessageItem first, TemplateMessageItem remark)
        {
            Add("first",first);
            Add("remark",remark);
        }

        /// <summary>
        /// 添加新的关键字内容。
        /// </summary>
        /// <param name="keyword">具体的关键字标识，例如 keyword1，具体视模板情况而定。</param>
        /// <param name="value">关键字的内容。</param>
        /// <param name="valueColor">关键字的展示颜色。</param>
        public TemplateMessage AddKeywords(string keyword, string value, Color valueColor)
        {
            Add(keyword,new TemplateMessageItem(value,valueColor));
            return this;
        }

        /// <summary>
        /// 添加新的关键字内容。
        /// </summary>
        /// <param name="keyword">具体的关键字标识，例如 keyword1，具体视模板情况而定。</param>
        /// <param name="value">关键字的内容。</param>
        /// <param name="valueColorStr">关键字的展示颜色。</param>
        public TemplateMessage AddKeywords(string keyword, string value, string valueColorStr)
        {
            Add(keyword,new TemplateMessageItem(value,valueColorStr));
            return this;
        }

        /// <summary>
        /// 添加新的关键字内容，颜色为默认的黑色。
        /// </summary>
        /// <param name="keyword">具体的关键字标识，例如 keyword1，具体视模板情况而定。</param>
        /// <param name="value">关键字的内容。</param>
        public TemplateMessage AddKeywords(string keyword, string value)
        {
            Add(keyword,new TemplateMessageItem(value,Color.Black));
            return this;
        }
    }
}