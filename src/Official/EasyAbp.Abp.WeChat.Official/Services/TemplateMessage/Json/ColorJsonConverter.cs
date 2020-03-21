using System;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Json
{
    /// <summary>
    /// 针对于 <see cref="Color"/> 类型定义的 Json 转换器，用于将颜色实例转换为 16 进制字符串。
    /// 并且实现了从十六进制字符串，转换为 <see cref="Color"/> 实例的功能。
    /// </summary>
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteValue("#" + value.R.ToString("x2") + value.G.ToString("x2") + value.B.ToString("x2"));
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is string valueStr)
            {
                return Color.FromArgb(int.Parse(valueStr.Replace("#", ""),NumberStyles.HexNumber));
            }

            return Color.Black;
        }
    }
}