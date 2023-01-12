using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Json;

/// <summary>
/// 针对于 <see cref="Color"/> 类型定义的 Json 转换器，用于将颜色实例转换为 16 进制字符串。
/// 并且实现了从十六进制字符串，转换为 <see cref="Color"/> 实例的功能。
/// </summary>
public class TextJsonColorJsonConverter : JsonConverter<Color>
{
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue("#" + value.R.ToString("x2") + value.G.ToString("x2") + value.B.ToString("x2"));
    }

    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return !value.IsNullOrEmpty()
            ? Color.FromArgb(int.Parse(value!.Replace("#", ""), NumberStyles.HexNumber))
            : Color.Black;
    }
}