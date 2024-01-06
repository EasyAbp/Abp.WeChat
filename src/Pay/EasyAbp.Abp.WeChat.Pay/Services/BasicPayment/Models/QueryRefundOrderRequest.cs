using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class QueryRefundOrderRequest
{
    /// <summary>
    /// 商户退款单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [Required]
    [StringLength(64, MinimumLength = 1)]
    [JsonProperty("out_refund_no")]
    public string OutRefundNo { get; set; }
}