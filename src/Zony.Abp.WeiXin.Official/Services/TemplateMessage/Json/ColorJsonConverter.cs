using System;
using System.Drawing;
using Newtonsoft.Json;

namespace Zony.Abp.WeiXin.Official.Services.TemplateMessage.Json
{
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
                return Color.FromArgb(int.Parse(valueStr.Replace("#", "")));
            }

            return Color.Black;
        }
    }
}