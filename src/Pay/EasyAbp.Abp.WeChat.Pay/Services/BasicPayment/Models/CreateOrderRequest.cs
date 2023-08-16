using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class CreateOrderRequest
{
    /// <summary>
    /// 应用 ID。
    /// </summary>
    /// <remarks>
    /// 由微信生成的应用 ID，全局唯一。请求基础下单接口时请注意 APPID 的应用属性。<br/>
    /// 例如公众号场景下，需使用应用属性为公众号的服务号 APPID。
    /// </remarks>
    /// <example>
    /// 示例值：wxd678efh567hg6787。
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
    /// 示例值：1900000109。
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("mchid")]
    public string MchId { get; set; }

    /// <summary>
    /// 商品描述。
    /// </summary>
    /// <remarks>
    /// 商品描述。
    /// </remarks>
    /// <example>
    /// 示例值：Image形象店-深圳腾大-QQ公仔。
    /// </example>
    [Required]
    [StringLength(127, MinimumLength = 1)]
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// 商户订单号。
    /// </summary>
    /// <remarks>
    /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一。
    /// </remarks>
    /// <example>
    /// 示例值: 20150806125346。
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 6)]
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 交易结束时间。
    /// </summary>
    /// <remarks>
    /// 订单失效时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE。<br/>
    /// 开发人员只需要传递 DateTime 对象，底层的 Newtonsoft.Json 库会自动将其转换为符合微信支付要求的格式。
    /// </remarks>
    /// <example>
    /// 示例值: 2018-06-08T10:34:56+08:00
    /// </example>
    [JsonProperty("time_expire")]
    public DateTime? TimeExpire { get; set; }

    /// <summary>
    /// 附加数据。
    /// </summary>
    /// <remarks>
    /// 附加数据，在查询 API 和支付通知中原样返回，可作为自定义参数使用，实际情况下只有支付完成状态才会返回该字段。
    /// </remarks>
    /// <example>
    /// 示例值：自定义数据  
    /// </example>
    [JsonProperty("attach")]
    [StringLength(128, MinimumLength = 1)]
    public string Attach { get; set; }

    /// <summary>
    /// 通知地址。
    /// </summary>
    /// <remarks>
    /// 异步接收微信支付结果通知的回调地址，通知 URL 必须为外网可访问的 URL，不能携带参数。<br/>
    /// 公网域名必须为 HTTPS，如果是走专线接入，使用专线 NAT IP 或者私有回调域名可使用 HTTP。
    /// </remarks>
    /// <example>
    /// 示例值: https://www.weixin.qq.com/wxpay/pay.php
    /// </example>
    [Required]
    [StringLength(256, MinimumLength = 1)]
    [JsonProperty("notify_url")]
    public string NotifyUrl { get; set; }

    /// <summary>
    /// 订单优惠标记。
    /// </summary>
    /// <remarks>
    /// 订单优惠标记。
    /// </remarks>
    /// <example>
    /// 示例值：WXG。
    /// </example>
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("goods_tag")]
    public string GoodsTag { get; set; }

    /// <summary>
    /// 电子发票入口开放标识。
    /// </summary>
    /// <remarks>
    /// 传入 true 时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效。<br/>
    /// true: 是<br/>
    /// false: 否<br/>
    /// </remarks>
    /// <example>
    /// 示例值: true
    /// </example>
    [JsonProperty("support_fapiao")]
    public bool SupportFaPiao { get; set; }

    /// <summary>
    /// 订单金额信息。
    /// </summary>
    [Required]
    [NotNull]
    [JsonProperty("amount")]
    public CreateOrderAmountModel Amount { get; set; }

    /// <summary>
    /// 优惠功能。
    /// </summary>
    [JsonProperty("detail")]
    public CreateOrderDetailModel Detail { get; set; }

    /// <summary>
    /// 场景信息。
    /// </summary>
    /// <remarks>
    /// 支付场景描述。
    /// </remarks>
    [JsonProperty("scene_info")]
    public CreateOrderSceneInfoModel SceneInfo { get; set; }

    public class CreateOrderSceneInfoModel
    {
        /// <summary>
        /// 用户终端 IP。
        /// </summary>
        /// <remarks>
        /// 用户的客户端 IP，支持 IPv4 和 IPv6 两种格式的 IP 地址。
        /// </remarks>
        /// <example>
        /// 示例值: 14.23.150.211
        /// </example>
        [Required]
        [StringLength(45, MinimumLength = 1)]
        [JsonProperty("payer_client_ip")]
        public string PayerClientIp { get; set; }

        /// <summary>
        /// 商户端设备号。
        /// </summary>
        /// <remarks>
        /// 商户端设备号（门店号或收银设备 ID）。
        /// </remarks>
        /// <example>
        /// 示例值: 013467007045764。
        /// </example>
        [StringLength(32, MinimumLength = 1)]
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 商户门店信息。
        /// </summary>
        [JsonProperty("store_info")]
        public CreateOrderStoreInfoModel StoreInfo { get; set; }

        public class CreateOrderStoreInfoModel
        {
            /// <summary>
            /// 门店编号。
            /// </summary>
            /// <remarks>
            /// 商户侧门店编号。
            /// </remarks>
            /// <example>
            /// 示例值: 0001
            /// </example>
            [Required]
            [StringLength(32, MinimumLength = 1)]
            [JsonProperty("id")]
            public string Id { get; set; }

            /// <summary>
            /// 门店名称。
            /// </summary>
            /// <remarks>
            /// 商户侧门店名称。
            /// </remarks>
            /// <example>
            /// 示例值: 腾讯大厦分店
            /// </example>
            [JsonProperty("name")]
            [StringLength(256, MinimumLength = 1)]
            public string Name { get; set; }

            /// <summary>
            /// 地区编码。
            /// </summary>
            /// <remarks>
            /// 地区编码，详细请见省市区编号对照表(https://pay.weixin.qq.com/wiki/doc/apiv3/wxpay/ecommerce/applyments/chapter4_1.shtml)。
            /// </remarks>
            /// <example>
            /// 示例值: 440305
            /// </example>
            [JsonProperty("area_code")]
            [StringLength(32, MinimumLength = 1)]
            public string AreaCode { get; set; }

            /// <summary>
            /// 详细地址。
            /// </summary>
            /// <remarks>详细的商户门店地址。</remarks>
            /// <example>示例值: 广东省深圳市南山区科技中一道 10000 号</example>
            [JsonProperty("address")]
            [StringLength(512, MinimumLength = 1)]
            public string Address { get; set; }
        }
    }

    /// <summary>
    /// 结算信息。
    /// </summary>
    [JsonProperty("settle_info")]
    public CreateOrderSettleInfoModel SettleInfo { get; set; }

    public class CreateOrderSettleInfoModel
    {
        /// <summary>
        /// 是否指定分账。
        /// </summary>
        /// <example>
        /// 示例值: false
        /// </example>
        [JsonProperty("profit_sharing")]
        public bool ProfitSharing { get; set; }
    }
}

public class CreateOrderAmountModel
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
    [Required]
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// 货币类型。
    /// </summary>
    /// <remarks>
    /// CNY: 人民币，境内商户号仅支持人民币。
    /// </remarks>
    /// <example>
    /// 示例值: CNY
    /// </example>
    [StringLength(16, MinimumLength = 1)]
    [JsonProperty("currency")]
    public string Currency { get; set; } = "CNY";
}



public class CreateOrderDetailModel
{
    /// <summary>
    /// 订单原价。
    /// </summary>
    /// <remarks>
    /// 1. 商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的交易金额。<br/>
    /// 2. 当订单原价与支付金额不相等，则不享受优惠。<br/>
    /// 3. 该字段主要用于防止同一张小票分多次支付，以享受多次优惠的情况，正常支付订单不必上传此参数。
    /// </remarks>
    /// <example>
    /// 示例值: 608800
    /// </example>
    [JsonProperty("cost_price")]
    public int CostPrice { get; set; }

    /// <summary>
    /// 商家小票 Id。
    /// </summary>
    /// <remarks>
    /// 商家小票 Id。
    /// </remarks>
    /// <example>
    /// 示例值: 微信123
    /// </example>
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("invoice_id")]
    public string InvoiceId { get; set; }

    public GoodsDetailModel GoodsDetail { get; set; }

    public class GoodsDetailModel
    {
        /// <summary>
        /// 商户测商品编码。
        /// </summary>
        /// <remarks>
        /// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
        /// </remarks>
        /// <example>
        /// 示例值: 1246464644
        /// </example>
        [Required]
        [StringLength(32, MinimumLength = 1)]
        [JsonProperty("merchant_goods_id")]
        public string MerchantGoodsId { get; set; }

        /// <summary>
        /// 微信支付商品编码。
        /// </summary>
        /// <remarks>
        /// 微信支付定义的统一商品编号（没有可不传）。
        /// </remarks>
        /// <example>
        /// 示例值: 1001
        /// </example>
        [StringLength(32, MinimumLength = 1)]
        [JsonProperty("wechatpay_goods_id")]
        public string WeChatPayGoodsId { get; set; }

        /// <summary>
        /// 商品名称。
        /// </summary>
        /// <remarks>
        /// 商品的实际名称。
        /// </remarks>
        /// <example>
        /// 示例值: iPhoneX 256G
        /// </example>
        [StringLength(256, MinimumLength = 1)]
        [JsonProperty("goods_name")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品数量。
        /// </summary>
        /// <remarks>
        /// 用户购买的数量。
        /// </remarks>
        /// <example>
        /// 示例值: 1
        /// </example>
        [Required]
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 商品单价。
        /// </summary>
        /// <remarks>
        /// 单位为: 分。如果商户有优惠，需传输商户优惠后的单价(例如: 用户对一笔 100 元的订单<br/>
        /// 使用了商场发的纸质优惠券 100-50，则活动商品的单价应为原单价 -50)
        /// </remarks>
        /// <example>
        /// 示例值: 528800
        /// </example>
        [Required]
        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }
    }
}