using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;

public class QueryOrderByWechatNumberRequest
{
    /// <summary>
    /// 直连商户号。
    /// </summary>
    /// <remarks>
    /// 直连商户的商户号，由微信支付生成并下发。
    /// </remarks>
    /// <example>示例值: 1230000109。</example>
    [JsonProperty("mchid")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string MchId { get; set; }

    /// <summary>
    /// 微信支付订单号。
    /// </summary>
    /// <remarks>
    /// 微信支付系统生成的订单号。
    /// </remarks>
    /// <example>示例值: 1217752501201407033233368018。</example>
    [JsonProperty("transaction_id")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonIgnore]
    public string TransactionId { get; set; }
}