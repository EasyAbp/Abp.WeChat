using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class RefundOrderResponse : WeChatPayCommonErrorResponse
{
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
    /// 退款渠道。
    /// </summary>
    /// <remarks>
    /// 具体渠道请参考类型 <see cref="RefundChannelEnum"/> 的定义。
    /// </remarks>
    /// <example>
    /// 示例值: ORIGINAL (<see cref="RefundChannelEnum.Original"/>)
    /// </example>
    [JsonProperty("channel")]
    [Required]
    [StringLength(16, MinimumLength = 1)]
    public string Channel { get; set; }

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
    /// 退款状态。
    /// </summary>
    /// <remarks>
    /// 退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台-交易中心，手动处理此笔退款。<br/>
    /// 具体状态值可以参考类型 <see cref="RefundStatusEnum"/> 的定义。
    /// </remarks>
    /// <example>
    /// 示例值: SUCCESS
    /// </example>
    [JsonProperty("status")]
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Status { get; set; }

    /// <summary>
    /// 资金账户。
    /// </summary>
    /// <remarks>
    /// 退款所使用资金对应的资金账户类型，请参考类型 <see cref="RefundFundsAccountEnum"/> 的定义。
    /// </remarks>
    /// <example>
    /// 示例值: UNSETTLED (<see cref="RefundFundsAccountEnum.Unsettled"/>)
    /// </example>
    [JsonProperty("funds_account")]
    [StringLength(32, MinimumLength = 1)]
    public string FundsAccount { get; set; }

    /// <summary>
    /// 金额信息。
    /// </summary>
    /// <remarks>
    /// 金额详细信息。
    /// </remarks>
    [JsonProperty("amount")]
    [Required]
    public AmountInfo Amount { get; set; }

    /// <summary>
    /// 优惠退款信息。
    /// </summary>
    [JsonProperty("promotion_detail")]
    public List<PromotionDetailModel> PromotionDetail { get; set; }

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
        /// 退款出资账户及金额。
        /// </summary>
        /// <remarks>
        /// 退款出资的账户类型及金额信息。
        /// </remarks>
        [JsonProperty("from")]
        public List<RefundOrderRequest.AmountInfo.RefundSource> From { get; set; }

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

        /// <summary>
        /// 应结退款金额。
        /// </summary>
        /// <remarks>
        /// 去掉非充值代金券退款金额后的退款金额，单位为分，退款金额=申请退款金额-非充值代金券退款金额，退款金额小于等于申请退款金额。
        /// </remarks>
        /// <example>示例值: 100</example>
        [JsonProperty("settlement_refund")]
        [Required]
        public int SettlementRefund { get; set; }

        /// <summary>
        /// 应结订单金额。
        /// </summary>
        /// <remarks>
        /// 应结订单金额=订单金额-免充值代金券金额，应结订单金额小于等于订单金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 100
        /// </example>
        [JsonProperty("settlement_total")]
        [Required]
        public int SettlementTotal { get; set; }

        /// <summary>
        /// 优惠退款金额。
        /// </summary>
        /// <remarks>
        /// 优惠退款金额小于等于退款金额，退款金额-代金券或立减优惠退款金额为现金，说明详见代金券或立减优惠，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 10
        /// </example>
        [JsonProperty("discount_refund")]
        [Required]
        public int DiscountRefund { get; set; }

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
        [StringLength(16)]
        public string Currency { get; set; }

        /// <summary>
        /// 手续费退款金额。
        /// </summary>
        /// <remarks>
        /// 手续费退款金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 10
        /// </example>
        [JsonProperty("refund_fee")]
        public int? RefundFee { get; set; }
    }

    public class PromotionDetailModel
    {
        /// <summary>
        /// 券 ID。
        /// </summary>
        /// <remarks>
        /// 券或者立减优惠 ID。
        /// </remarks>
        /// <example>
        /// 示例值: 109519
        /// </example>
        [JsonProperty("promotion_id")]
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string PromotionId { get; set; }

        /// <summary>
        /// 优惠范围。
        /// </summary>
        /// <remarks>
        /// 枚举值，参考类型 <see cref="PromotionScopeEnum"/> 。
        /// </remarks>
        /// <example>
        /// 示例值: SINGLE (<see cref="PromotionScopeEnum.Single"/>)
        /// </example>
        [JsonProperty("scope")]
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Scope { get; set; }

        /// <summary>
        /// 优惠类型。
        /// </summary>
        /// <remarks>
        /// 枚举值，参考类型 <see cref="PromotionTypeEnum"/> 。
        /// </remarks>
        /// <example>
        /// 示例值: DISCOUNT (<see cref="PromotionTypeEnum.Discount"/>)
        /// </example>
        [JsonProperty("type")]
        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Type { get; set; }

        /// <summary>
        /// 优惠券面额。
        /// </summary>
        /// <remarks>
        /// 用户享受优惠的金额（优惠券面额=微信出资金额+商家出资金额+其他出资方金额 ），单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 5
        /// </example>
        [JsonProperty("amount")]
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// 优惠退款金额。
        /// </summary>
        /// <remarks>
        /// 优惠退款金额小于等于退款金额，退款金额-代金券或立减优惠退款金额为用户支付的现金，说明详见代金券或立减优惠，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 100
        /// </example>
        [JsonProperty("refund_amount")]
        [Required]
        public int RefundAmount { get; set; }

        /// <summary>
        /// 商品列表。
        /// </summary>
        /// <remarks>
        /// 优惠商品发生退款时返回商品信息。
        /// </remarks>
        [JsonProperty("goods_detail")]
        public List<GoodsDetailModel> GoodsDetail { get; set; }

        public class GoodsDetailModel
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
            [StringLength(32, MinimumLength = 1)]
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
            [StringLength(32, MinimumLength = 1)]
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
            [StringLength(256, MinimumLength = 1)]
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
}