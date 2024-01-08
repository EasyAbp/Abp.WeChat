using System.Text.Json.Serialization;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class PaymentNotifyCallbackResponse
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