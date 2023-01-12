using System.Drawing;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 微信模板消息的数据项定义，一个模板消息是由多个 <see cref="TemplateMessageItem"/> 所构成的。
    /// </summary>
    public class TemplateMessageItem
    {
        public TemplateMessageItem()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="TemplateMessageItem"/> 实例。
        /// </summary>
        /// <param name="value">模板关键字的填充内容。</param>
        /// <param name="color">模板关键字的展示颜色。</param>
        public TemplateMessageItem(string value, Color color)
        {
            Value = value;
            Color = color;
        }

        public TemplateMessageItem(string value)
        {
            Value = value;
            Color = Color.Black;
        }

        /// <summary>
        /// 构建一个新的 <see cref="TemplateMessageItem"/> 实例。
        /// </summary>
        /// <param name="value">模板关键字的填充内容。</param>
        /// <param name="color">模板关键字的展示颜色，此处的颜色必须传入 16 进制颜色值。例如 #0e53dc 这种字符串。</param>
        public TemplateMessageItem(string value, string color)
        {
            Value = value;
            Color = Color.FromArgb(int.Parse(color.Replace("#", "")));
        }

        /// <summary>
        /// 模板关键字的填充内容。
        /// </summary>
        [JsonPropertyName("value")]
        [JsonProperty("value")]
        public string Value { get; private set; }

        /// <summary>
        /// 模板关键字的展示颜色。
        /// </summary>
        [JsonPropertyName("color")]
        [JsonProperty("color")]
        [Newtonsoft.Json.JsonConverter(typeof(NewtonsoftJsonColorJsonConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(TextJsonColorJsonConverter))]
        public Color Color { get; private set; }
    }
}