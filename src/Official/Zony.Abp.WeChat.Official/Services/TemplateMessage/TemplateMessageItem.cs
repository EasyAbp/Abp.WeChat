using System.Drawing;
using Newtonsoft.Json;
using Zony.Abp.WeChat.Official.Services.TemplateMessage.Json;

namespace Zony.Abp.WeChat.Official.Services.TemplateMessage
{
    public class TemplateMessageItem
    {
        public TemplateMessageItem(string value,Color color)
        {
            Value = value;
            Color = color;
        }

        public TemplateMessageItem(string value, string color)
        {
            Value = value;
            Color = Color.FromArgb(int.Parse(color.Replace("#", "")));
        }
        
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("color")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color Color { get; set; }
    }
}