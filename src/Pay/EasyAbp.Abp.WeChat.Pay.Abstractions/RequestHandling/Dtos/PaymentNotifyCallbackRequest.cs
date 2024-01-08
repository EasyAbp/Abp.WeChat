using System;
using System.Text.Json.Serialization;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class PaymentNotifyCallbackRequest
{
    /// <summary>
    /// 通知 ID。
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// 通知创建时间。
    /// </summary>
    [JsonPropertyName("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 通知类型。
    /// </summary>
    [JsonPropertyName("event_type")]
    public string EventType { get; set; }

    /// <summary>
    /// 通知数据类型。
    /// </summary>
    [JsonPropertyName("resource_type")]
    public string ResourceType { get; set; }

    /// <summary>
    /// 回调摘要。
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// 通知数据。
    /// </summary>
    [JsonPropertyName("resource")]
    public ResourceModel Resource { get; set; }

    public class ResourceModel
    {
        /// <summary>
        /// 加密算法类型。
        /// </summary>
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        /// 数据密文。
        /// </summary>
        [JsonPropertyName("ciphertext")]
        public string Ciphertext { get; set; }

        /// <summary>
        /// 附加数据。
        /// </summary>
        [JsonPropertyName("associated_data")]
        public string AssociatedData { get; set; }

        /// <summary>
        /// 原始类型。
        /// </summary>
        [JsonPropertyName("original_type")]
        public string OriginalType { get; set; }

        /// <summary>
        /// 随机串。
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}