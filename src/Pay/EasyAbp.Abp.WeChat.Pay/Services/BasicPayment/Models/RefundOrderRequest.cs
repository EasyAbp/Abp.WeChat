using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class RefundOrderRequest
{
    /// <summary>
    /// 微信支付订单号。
    /// </summary>
    /// <remarks>
    /// 原支付交易对应的微信订单号。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018
    /// </example>
    [JsonProperty("transaction_id")]
    [StringLength(32, MinimumLength = 1)]
    public string TransactionId { get; set; }

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
    [StringLength(32, MinimumLength = 6)]
    public string OutTradeNo { get; set; }

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
    /// 退款原因。
    /// </summary>
    /// <remarks>
    /// 若商户传入，会在下发给用户的退款消息中体现退款原因。
    /// </remarks>
    /// <example>
    /// 示例值: 商品已售完
    /// </example>
    [JsonProperty("reason")]
    [StringLength(80)]
    public string Reason { get; set; }

    /// <summary>
    /// 退款结果回调 URL。	
    /// </summary>
    /// <remarks>
    /// 异步接收微信支付退款结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。<br/>
    /// 如果参数中传了 notify_url，则商户平台上配置的回调地址将不会生效，优先回调当前传的这个地址。
    /// </remarks>
    /// <example>
    /// 示例值: https://weixin.qq.com
    /// </example>
    [JsonProperty("notify_url")]
    [StringLength(256, MinimumLength = 8)]
    public string NotifyUrl { get; set; }

    /// <summary>
    /// 退款资金来源。
    /// </summary>
    /// <remarks>
    /// 若传递此参数则使用对应的资金账户退款，否则默认使用未结算资金退款 (仅对老资金流商户适用)。
    /// 仅支持取值为 <see cref="RefundRefundFundsAccountEnumable"/> 。
    /// </remarks>
    /// <example>
    /// 示例值: AVAILABLE (<see cref="RefundFundsAccountEnum.Available"/>)
    /// </example>
    [JsonProperty("funds_account")]
    [StringLength(32)]
    public string FundsAccount { get; set; }

    /// <summary>
    /// 金额信息。
    /// </summary>
    /// <remarks>
    /// 订单金额信息。
    /// </remarks>
    [JsonProperty("amount")]
    [Required]
    public AmountInfo Amount { get; set; }

    /// <summary>
    /// 退款商品。
    /// </summary>
    /// <remarks>
    /// 指定商品退款需要传此参数，其他场景无需传递。
    /// </remarks>
    [JsonProperty("goods_detail")]
    public List<GoodsDetail> GoodsDetails { get; set; }

    public class AmountInfo
    {
        /// <summary>
        /// 退款金额。
        /// </summary>
        /// <remarks>
        /// 退款金额，单位为分，只能为整数，不能超过原订单支付金额。
        /// </remarks>
        /// <example>
        /// 示例值: 888
        /// </example>
        [JsonProperty("refund")]
        [Required]
        public int Refund { get; set; }

        /// <summary>
        /// 退款出资账户及金额。
        /// </summary>
        /// <remarks>
        /// 退款需要从指定账户出资时，传递此参数指定出资金额（币种的最小单位，只能为整数）。<br/>
        /// 同时指定多个账户出资退款的使用场景需要满足以下条件: <br/>
        /// 1. 未开通退款支出分离产品功能; <br/>
        /// 2. 订单属于分账订单，且分账处于待分账或分账中状态。<br/>
        /// 参数传递需要满足条件: <br/>
        /// 1. 基本账户可用余额出资金额与基本账户不可用余额出资金额之和等于退款金额; <br/>
        /// 2. 账户类型不能重复。<br/>
        /// 上述任一条件不满足将返回错误。
        /// </remarks>
        [JsonProperty("from")]
        public List<RefundSource> RefundSources { get; set; }

        /// <summary>
        /// 原始订单金额。
        /// </summary>
        /// <remarks>
        /// 原支付交易的订单总金额，单位为分，只能为整数。
        /// </remarks>
        /// <example>
        /// 示例值: 888
        /// </example>
        [JsonProperty("total")]
        [Required]
        public int Total { get; set; }

        /// <summary>
        /// 退款币种。
        /// </summary>
        /// <remarks>
        /// 符合ISO 4217标准的三位字母代码，目前只支持人民币: CNY。
        /// </remarks>
        /// <example>
        /// 示例值: CNY
        /// </example>
        [JsonProperty("currency")]
        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string Currency { get; set; }

        public class RefundSource
        {
            /// <summary>
            /// 出资账户类型。
            /// </summary>
            /// <remarks>
            /// 参考类型 <see cref="RefundFundsAccountEnum"/> 的定义。
            /// </remarks>
            /// <example>
            /// 示例值: AVAILABLE (<see cref="RefundFundsAccountEnum.Available"/>)
            /// </example>
            [JsonProperty("account")]
            [Required]
            [StringLength(32, MinimumLength = 1)]
            public string Account { get; set; }

            /// <summary>
            /// 出资金额。
            /// </summary>
            /// <remarks>
            /// 对应账户出资金额。
            /// </remarks>
            /// <example>
            /// 示例值: 444
            /// </example>
            [JsonProperty("amount")]
            [Required]
            public int Amount { get; set; }
        }
    }

    public class GoodsDetail
    {
        /// <summary>
        /// 商户侧商品编码。
        /// </summary>
        /// <remarks>
        /// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
        /// </remarks>
        /// <example>
        /// 示例值: 1217752501201407033233368018
        /// </example>
        [JsonProperty("merchant_goods_id")]
        [Required]
        [StringLength(32)]
        public string MerchantGoodsId { get; set; }

        /// <summary>
        /// 微信支付商品编码。
        /// </summary>
        /// <remarks>
        /// 微信支付定义的统一商品编号(没有可不传)。
        /// </remarks>
        /// <example>
        /// 示例值: 1001
        /// </example>
        [JsonProperty("wechatpay_goods_id")]
        [StringLength(32)]
        public string WechatPayGoodsId { get; set; }

        /// <summary>
        /// 商品名称。
        /// </summary>
        /// <remarks>
        /// 商品的实际名称。
        /// </remarks>
        /// <example>
        /// 示例值: iPhone6s 16G
        /// </example>
        [JsonProperty("goods_name")]
        [StringLength(256)]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品单价。
        /// </summary>
        /// <remarks>
        /// 商品单价金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 528800
        /// </example>
        [JsonProperty("unit_price")]
        [Required]
        public int UnitPrice { get; set; }

        /// <summary>
        /// 商品退款金额。
        /// </summary>
        /// <remarks>
        /// 商品退款金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 528800
        /// </example>
        [JsonProperty("refund_amount")]
        [Required]
        public int RefundAmount { get; set; }

        /// <summary>
        /// 商品退货数量。 
        /// </summary>
        /// <remarks>
        /// 单品的退款数量。
        /// </remarks>
        /// <example>
        /// 示例值: 1
        /// </example>
        [JsonProperty("refund_quantity")]
        [Required]
        public int RefundQuantity { get; set; }
    }
}