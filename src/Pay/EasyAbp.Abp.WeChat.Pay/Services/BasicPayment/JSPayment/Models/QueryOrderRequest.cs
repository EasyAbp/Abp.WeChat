using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;

public class QueryOrderRequest
{
    /// <summary>
    /// 直连商户号。
    /// </summary>
    /// <remarks>
    /// 直连商户的商户号，由微信支付生成并下发。
    /// </remarks>
    /// <example>示例值: 1230000109。</example>
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
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string TransactionId { get; set; }
}