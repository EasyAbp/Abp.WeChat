using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;

public class WeChatPayRefundEventModel
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
    /// 原支付交易对应的商户订单号。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [JsonProperty("out_trade_no")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 微信支付订单号。
    /// </summary>
    /// <remarks>
    /// 微信支付交易订单号。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [JsonProperty("transaction_id")]
    [Required]
    [StringLength(32, MinimumLength = 1)]

    public string TransactionId { get; set; }

    /// <summary>
    /// 商户退款单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [JsonProperty("out_refund_no")]
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public string OutRefundNo { get; set; }

    /// <summary>
    /// 微信支付退款单号。
    /// </summary>
    /// <remarks>
    /// 微信支付退款单号。
    /// </remarks>
    /// <example>
    /// 示例值: 50000000382019052709732678859
    /// </example>
    [JsonProperty("refund_id")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string RefundId { get; set; }

    /// <summary>
    /// 退款状态。
    /// </summary>
    /// <remarks>
    /// 退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台-交易中心，手动处理此笔退款。<br/>
    /// 具体状态值可以参考类型 <see cref="RefundStatusEnum"/> 的定义（事件中不会包含 PROCESSING 状态）。
    /// </remarks>
    /// <example>
    /// 示例值: SUCCESS
    /// </example>
    [JsonProperty("refund_status")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string RefundStatus { get; set; }

    /// <summary>
    /// 退款成功时间。
    /// </summary>
    /// <remarks>
    /// 退款成功时间，当退款状态为退款成功时有返回。
    /// </remarks>
    /// <example>
    /// 示例值: 2020-12-01T16:18:12+08:00
    /// </example>
    [JsonProperty("success_time")]
    [StringLength(64, MinimumLength = 1)]
    public DateTime? SuccessTime { get; set; }

    /// <summary>
    /// 退款入账账户。
    /// </summary>
    /// <remarks>
    /// 取当前退款单的退款入账方，有以下几种情况: <br/>
    /// 1. 退回银行卡: {银行名称}{卡类型}{卡尾号} <br/>
    /// 2. 退回支付用户零钱: 支付用户零钱 <br/>
    /// 3. 退还商户: 商户基本账户商户结算银行账户 <br/>
    /// 4. 退回支付用户零钱通:支付用户零钱通
    /// </remarks>
    /// <remarks>
    /// 示例值: 招商银行信用卡0403
    /// </remarks>
    [JsonProperty("user_received_account")]
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public string UserReceivedAccount { get; set; }

    /// <summary>
    /// 退款创建时间。
    /// </summary>
    /// <remarks>
    /// 退款受理时间。
    /// </remarks>
    /// <example>示例值: 2020-12-01T16:18:12+08:00</example>
    [JsonProperty("create_time")]
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 金额信息。
    /// </summary>
    /// <remarks>
    /// 金额详细信息。
    /// </remarks>
    [JsonProperty("amount")]
    [Required]
    public AmountInfo Amount { get; set; }

    public class AmountInfo
    {
        /// <summary>
        /// 订单金额。
        /// </summary>
        /// <remarks>
        /// 订单总金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 100
        /// </example>
        [JsonProperty("total")]
        [Required]
        public int Total { get; set; }

        /// <summary>
        /// 退款金额。
        /// </summary>
        /// <remarks>
        /// 退款标价金额，单位为分，可以做部分退款。
        /// </remarks>
        /// <example>
        /// 示例值: 100
        /// </example>
        [JsonProperty("refund")]
        [Required]
        public int Refund { get; set; }

        /// <summary>
        /// 用户支付金额。
        /// </summary>
        /// <remarks>
        /// 现金支付金额，单位为分，只能为整数。
        /// </remarks>
        /// <example>
        /// 示例值: 90
        /// </example>
        [JsonProperty("payer_total")]
        [Required]
        public int PayerTotal { get; set; }

        /// <summary>
        /// 用户退款金额。
        /// </summary>
        /// <remarks>
        /// 退款给用户的金额，不包含所有优惠券金额。
        /// </remarks>
        /// <example>
        /// 示例值: 90
        /// </example>
        [JsonProperty("payer_refund")]
        [Required]
        public int PayerRefund { get; set; }
    }
}