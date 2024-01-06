using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class QueryOrderResponse : WeChatPayCommonErrorResponse
{
    /// <summary>
    /// 应用 ID。
    /// </summary>
    /// <remarks>
    /// 直连商户申请的公众号或移动应用 appid。
    /// </remarks>
    /// <example>
    /// 示例值: wxd678efh567hg6787。
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("appid")]
    public string AppId { get; set; }

    /// <summary>
    /// 直连商户号。
    /// </summary>
    /// <remarks>
    /// 直连商户的商户号，由微信支付生成并下发。
    /// </remarks>
    /// <example>
    /// 示例值: 1230000109。
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("mchid")]
    public string MchId { get; set; }

    /// <summary>
    /// 商户订单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一，详见【商户订单号】。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018。
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 6)]
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 微信支付订单号。
    /// </summary>
    /// <remarks>
    /// 微信支付系统生成的订单号。
    /// </remarks>
    /// <example>
    /// 示例值: 1217752501201407033233368018。
    /// </example>
    [StringLength(32)]
    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; }

    /// <summary>
    /// 交易类型。
    /// </summary>
    /// <remarks>
    /// 交易类型为美剧值，具体值可参考 <see cref="TradeTypeEnum"/> 中找到对应的定义。
    /// </remarks>
    /// <example>
    /// 示例值: MICROPAY。(<see cref="TradeTypeEnum"/>)。
    /// </example>
    [StringLength(16)]
    [JsonProperty("trade_type")]
    public string TradeType { get; set; }

    /// <summary>
    /// 交易状态。
    /// </summary>
    /// <remarks>
    /// 交易状态为枚举值，具体值可以参考 <see cref="TradeStateEnum"/> 中找到对应的定义。
    /// </remarks>
    /// <example>
    /// 示例值: SUCCESS(<see cref="TradeStateEnum.Success"/>)。
    /// </example>
    [Required]
    [StringLength(32)]
    [JsonProperty("trade_state")]
    public string TradeState { get; set; }

    /// <summary>
    /// 交易状态描述。
    /// </summary>
    /// <remarks>
    /// 交易状态描述。
    /// </remarks>
    /// <example>
    /// 示例值: 支付成功。
    /// </example>
    [Required]
    [StringLength(256)]
    [JsonProperty("trade_state_desc")]
    public string TradeStateDesc { get; set; }

    /// <summary>
    /// 付款银行。
    /// </summary>
    /// <remarks>
    /// 银行类型，采用字符串类型的银行标识。银行标识请参考 <a href="https://pay.weixin.qq.com/wiki/doc/apiv3/terms_definition/chapter1_1_3.shtml#part-6">《银行类型对照表》</a>。
    /// </remarks>
    /// <example>
    /// 示例值: CMC。
    /// </example>
    [StringLength(32)]
    [JsonProperty("bank_type")]
    public string BankType { get; set; }

    /// <summary>
    /// 附加数据。
    /// </summary>
    /// <remarks>
    /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用，实际情况下只有支付完成状态才会返回该字段。
    /// </remarks>
    /// <example>
    /// 示例值: 自定义数据。
    /// </example>
    [StringLength(128)]
    [JsonProperty("attach")]
    public string Attach { get; set; }

    /// <summary>
    /// 支付完成时间。
    /// </summary>
    /// <remarks>
    /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE。<br/>
    /// 开发人员只需要传递 DateTime 对象，底层的 Newtonsoft.Json 库会自动将其转换为符合微信支付要求的格式。
    /// </remarks>
    /// <example>
    /// 示例值: 2018-06-08T10:34:56+08:00。
    /// </example>
    [StringLength(64)]
    [JsonProperty("success_time")]
    public DateTime? SuccessTime { get; set; }

    /// <summary>
    /// 支付者。
    /// </summary>
    /// <remarks>
    /// 支付者信息。
    /// </remarks>
    [Required]
    [JsonProperty("payer")]
    public QueryOrderPayerModel Payer { get; set; }

    /// <summary>
    /// 订单金额。
    /// </summary>
    /// <remarks>
    /// 订单金额信息，当支付成功时返回该字段。
    /// </remarks>
    [JsonProperty("amount")]
    public QueryOrderAmountModel Amount { get; set; }

    /// <summary>
    /// 场景信息。
    /// </summary>
    /// <remarks>
    /// 支付场景描述。
    /// </remarks>
    [JsonProperty("scene_info")]
    public QueryOrderSceneInfoModel SceneInfo { get; set; }

    /// <summary>
    /// 优惠功能。
    /// </summary>
    /// <remarks>
    /// 优惠功能，享受优惠时返回该字段。
    /// </remarks>
    [JsonProperty("promotion_detail")]
    public List<QueryOrderPromotionDetailModel> PromotionDetails { get; set; }

    public class QueryOrderPayerModel
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        /// <remarks>
        /// 用户在直连商户 appid 下的唯一标识。
        /// </remarks>
        /// <example>
        /// 示例值: oUpF8uMuAJO_M2pxb1Q9zNjWeS6o。
        /// </example>
        [Required]
        [StringLength(128, MinimumLength = 1)]
        [JsonProperty("openid")]
        public string OpenId { get; set; }
    }

    public class QueryOrderAmountModel
    {
        /// <summary>
        /// 总金额。
        /// </summary>
        /// <remarks>
        /// 订单总金额，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 100。
        /// </example>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// 用户支付金额。
        /// </summary>
        /// <remarks>
        /// 用户支付金额，单位为分。(指使用优惠券的情况下，这里等于总金额-优惠券金额)
        /// </remarks>
        /// <example>
        /// 示例值: 100。
        /// </example>
        [JsonProperty("payer_total")]
        public int PayerTotal { get; set; }

        /// <summary>
        /// 货币类型。
        /// </summary>
        /// <remarks>
        /// CNY: 人民币，境内商户号仅支持人民币。
        /// </remarks>
        /// <example>
        /// 示例值: CNY。
        /// </example>
        [StringLength(16)]
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 用户支付币种。
        /// </summary>
        /// <remarks>
        /// 用户支付币种。
        /// </remarks>
        /// <example>
        /// 示例值: CNY。
        /// </example>
        [StringLength(16)]
        [JsonProperty("payer_currency")]
        public string PayerCurrency { get; set; }
    }

    public class QueryOrderSceneInfoModel
    {
        /// <summary>
        /// 商户端设备号。
        /// </summary>
        /// <remarks>
        /// 商户端设备号 (发起扣款请求的商户服务器设备号)。
        /// </remarks>
        /// <example>
        /// 示例值: 013467007045764。
        /// </example>
        [StringLength(32)]
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
    }
}

public class QueryOrderPromotionDetailModel
{
    /// <summary>
    /// 券 ID。
    /// </summary>
    /// <example>
    /// 示例值: 109519。
    /// </example>
    [Required]
    [StringLength(32)]
    [JsonProperty("coupon_id")]
    public string CouponId { get; set; }

    /// <summary>
    /// 优惠名称。
    /// </summary>
    /// <example>
    /// 示例值: 单品惠-6。
    /// </example>
    [StringLength(64)]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 优惠范围。
    /// </summary>
    /// <remarks>
    /// 枚举值，取值范围请参考 <see cref="PromotionScopeEnum"/>。
    /// </remarks>
    /// <example>
    /// 示例值: GLOBAL。(<see cref="PromotionScopeEnum.Global"/>)
    /// </example>
    [StringLength(32)]
    [JsonProperty("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// 优惠类型。
    /// </summary>
    /// <remarks>
    /// 枚举值，取值范围请参考 <see cref="PromotionTypeEnum"/>。
    /// </remarks>
    /// <example>
    /// 示例值: CASH。(<see cref="PromotionTypeEnum.Cash"/>)
    /// </example>
    [StringLength(32)]
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// 优惠券面额。
    /// </summary>
    /// <example>
    /// 示例值: 100。
    /// </example>
    [Required]
    [JsonProperty("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// 活动 ID。
    /// </summary>
    /// <example>
    /// 示例值: 931386。
    /// </example>
    [StringLength(32)]
    [JsonProperty("stock_id")]
    public string StockId { get; set; }

    /// <summary>
    /// 微信出资。
    /// </summary>
    /// <remarks>
    /// 微信出资，单位为分。
    /// </remarks>
    /// <example>
    /// 示例值: 0
    /// </example>
    [JsonProperty("wechatpay_contribute")]
    public int WechatpayContribute { get; set; }

    /// <summary>
    /// 商户出资。
    /// </summary>
    /// <remarks>
    /// 商户出资，单位为分。
    /// </remarks>
    /// <example>
    /// 示例值: 0
    /// </example>
    [JsonProperty("merchant_contribute")]
    public int MerchantContribute { get; set; }

    /// <summary>
    /// 其他出资。
    /// </summary>
    /// <remarks>
    /// 其他出资，单位为分。
    /// </remarks>
    /// <example>
    /// 示例值: 0
    /// </example>
    [JsonProperty("other_contribute")]
    public int OtherContribute { get; set; }

    /// <summary>
    /// 优惠币种。
    /// </summary>
    /// <remarks>
    /// CNY: 人民币，境内商户号仅支持人民币。
    /// </remarks>
    /// <example>
    /// 示例值: CNY
    /// </example>
    [StringLength(16)]
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// 单品列表。
    /// </summary>
    /// <remarks>
    /// 单品列表信息。
    /// </remarks>
    [JsonProperty("goods_detail")]
    public List<QueryOrderPromotionDetailGoodsDetail> GoodsDetails { get; set; }

    public class QueryOrderPromotionDetailGoodsDetail
    {
        /// <summary>
        /// 商品编码。
        /// </summary>
        /// <example>
        /// 示例值: M1006
        /// </example>
        [Required]
        [StringLength(32)]
        [JsonProperty("goods_id")]
        public string GoodsId { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        /// <example>
        /// 用户购买的数量。
        /// </example>
        /// <example>
        /// 示例值: 1
        /// </example>
        [Required] [JsonProperty("quantity")] public int Quantity { get; set; }

        /// <summary>
        /// 商品单价。
        /// </summary>
        /// <remarks>
        /// 商品单价，单位为分。
        /// </remarks>
        /// <example>
        /// 示例值: 100
        /// </example>
        [Required]
        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }

        /// <summary>
        /// 商品优惠金额。
        /// </summary>
        /// <example>
        /// 示例值: 0
        /// </example>
        [Required]
        [JsonProperty("discount_amount")]
        public int DiscountAmount { get; set; }

        /// <summary>
        /// 商品备注。
        /// </summary>
        /// <example>
        /// 示例值: 商品备注信息
        /// </example>
        [StringLength(128)]
        [JsonProperty("goods_remark")]
        public string GoodsRemark { get; set; }
    }
}