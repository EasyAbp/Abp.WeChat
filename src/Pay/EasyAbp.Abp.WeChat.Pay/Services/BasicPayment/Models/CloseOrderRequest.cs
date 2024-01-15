using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class CloseOrderRequest
{
    /// <summary>
    /// 直连商户号。
    /// </summary>
    /// <remarks>
    /// 直连商户的商户号，由微信支付生成并下发。
    /// </remarks>
    /// <example>
    /// 示例值: 1230000109
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("mchid")]
    public string MchId { get; set; }

    /// <summary>
    /// 商户订单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 6)]
    [JsonProperty("out_trade_no")]
    [JsonIgnore]
    public string OutTradeNo { get; set; }
}