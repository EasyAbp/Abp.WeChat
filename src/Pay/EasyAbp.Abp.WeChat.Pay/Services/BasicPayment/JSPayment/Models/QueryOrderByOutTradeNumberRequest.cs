using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;

public class QueryOrderByOutTradeNumberRequest
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
    /// 商户订单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
    /// </remarks>
    /// <example>示例值: 1217752501201407033233368018。</example>
    [JsonProperty("out_trade_no")]
    [Required]
    [StringLength(32, MinimumLength = 6)]
    [JsonIgnore]
    public string OutTradeNo { get; set; }
}