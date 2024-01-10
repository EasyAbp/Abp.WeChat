using System;
using System.Text.Json.Serialization;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class WeChatPayNotificationOutput
{
    /// <summary>
    /// 返回状态码。
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// 返回信息。
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; }
}