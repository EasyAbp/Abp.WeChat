using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService.ParametersModel;

public class CreateCouponBatchRequest
{
    /// <summary>
    /// 批次名称。
    /// </summary>
    /// <remarks>
    /// 校验规则: <br/>
    /// 1. 批次名称最多 9 个中文汉字。<br/>
    /// 2. 批次名称最多 20 个字母。<br/>
    /// 3. 批次名称中不能包含不当内容和特殊字符 _ , ; | <br/>
    /// </remarks>
    /// <example>
    /// 示例值: 微信支付代金券批次。
    /// </example>
    [Required]
    [StringLength(20, MinimumLength = 1)]
    [JsonProperty("stock_name")]
    public string StockName { get; set; }

    /// <summary>
    /// 批次备注。
    /// </summary>
    /// <remarks>
    /// 仅制券商户可见，用于自定义信息。<br/>
    /// 校验规则:<br/>
    /// 批次备注最多 60 个 UTF8 字符数。
    /// </remarks>
    /// <example>
    /// 示例值: 零售批次。
    /// </example>
    [CanBeNull]
    [StringLength(60, MinimumLength = 1)]
    [JsonProperty("comment")]
    public string Comment { get; set; }

    /// <summary>
    /// 归属商户号。
    /// </summary>
    /// <remarks>
    /// 批次归属商户号。<br/>
    /// 本字段暂未开放生效，但入参时请设置为当前创建代金券商户号即不会报错，暂时不支持入参其他的商户号。
    /// </remarks>
    /// <example>
    /// 示例值: 98568865。
    /// </example>
    [Required]
    [StringLength(20, MinimumLength = 1)]
    [JsonProperty("belong_merchant")]
    public string BelongMerchant { get; set; }

    /// <summary>
    /// 可用时间-开始时间。
    /// </summary>
    /// <remarks>
    /// 批次开始时间，遵循 rfc3339 标准格式，格式为 yyyy-MM-DDTHH:mm:ss+TIMEZONE，yyyy-MM-DD表示年月日，T 出现在字符串中，表示 time 元素的开头，HH:mm:ss 表示时分秒，
    /// TIMEZONE 表示时区(+08:00 表示东八区时间，领先 UTC 8 小时，即北京时间)。<br/>
    /// 例如: 2015-05-20T13:29:35+08:00 表示，北京时间2015年5月20日 13点29分35秒。<br/>
    /// 校验规则：<br/>
    /// 1. 开始时间不可早于当前时间。<br/>
    /// 2. 不能创建 365 天后开始的批次。<br/>
    /// 3. 批次可用时间范围最长为 90 天。
    /// </remarks>
    /// <example>示例值: 2015-05-20T13:29:35+08:00</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("available_begin_time")]
    public DateTime AvailableBeginTime { get; set; }

    /// <summary>
    /// 可用时间-结束时间。
    /// </summary>
    /// <remarks>
    /// 批次结束时间，遵循 rfc3339 标准格式，格式为 yyyy-MM-DDTHH:mm:ss+TIMEZONE，yyyy-MM-DD表示年月日，T 出现在字符串中，表示 time 元素的开头，HH:mm:ss 表示时分秒，
    /// TIMEZONE 表示时区(+08:00 表示东八区时间，领先 UTC 8 小时，即北京时间)。<br/>
    /// 例如: 2015-05-20T13:29:35+08:00 表示，北京时间2015年5月20日 13点29分35秒。<br/>
    /// 校验规则：<br/>
    /// 1. 结束时间需晚于开始时间。<br/>
    /// 2. 可用时间最长为 90 天。<br/>
    /// 3. 有效时间间隔最短为 1s。
    /// </remarks>
    /// <example>示例值: 2015-05-20T13:29:35+08:00</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("available_end_time")]
    public DateTime AvailableEndTime { get; set; }

    /// <summary>
    /// 发放规则。
    /// </summary>
    /// <remarks>
    /// 批次使用规则。
    /// </remarks>
    [Required]
    [JsonProperty("stock_use_rule")]
    public InnerStockUseRule StockUseRule { get; set; }

    /// <summary>
    /// 样式设置。
    /// </summary>
    /// <remarks>
    /// 代金券详情页。
    /// </remarks>
    [JsonProperty("pattern_info")]
    [CanBeNull]
    public InnerPatternInfo PatternInfo { get; set; }

    /// <summary>
    /// 核销规则。
    /// </summary>
    [Required]
    [JsonProperty("coupon_use_rule")]
    public InnerCouponUseRule CouponUseRule { get; set; }

    /// <summary>
    /// 营销经费。
    /// </summary>
    /// <remarks>
    /// 营销经费。枚举值: <br/>
    /// true: 免充值<br/>
    /// false: 预充值<br/>
    /// 1. 免充值: 制券方无需提前充值资金，用户核销代金券时，直接从订单原价中扣除优惠减价金额，最终只将用户实际支付的金额结算给核销商户，商户实收少于订单原价。<br/>
    /// 2. 预充值: 制券方需将优惠预算提前充值到微信支付商户可用余额中，用户核销代金券时，系统从制券方商户可用余额中扣除优惠减价部分对应的资金，连同用户实际支付的资金，一并结算给核销商户，不影响实收。
    /// </remarks>
    /// <example>
    /// 示例值: false
    /// </example>
    [Required]
    [JsonProperty("no_cash")]
    public bool NoCash { get; set; }

    /// <summary>
    /// 批次类型。
    /// </summary>
    /// <remarks>
    /// 批次类型，仅支持: <br/>
    /// NORMAL: 固定面额满减券批次。
    /// </remarks>
    /// <example>
    /// 示例值: NORMAL
    /// </example>
    [Required]
    [StringLength(16, MinimumLength = 1)]
    [JsonProperty("stock_type")]
    public string StockType { get; set; }

    /// <summary>
    /// 商户单据号。
    /// </summary>
    /// <remarks>
    /// 商户创建批次凭据号（格式：商户 id + 日期 + 流水号），可包含英文字母，数字，|，_，*，-等内容，不允许出现其他不合法符号，商户侧需保持商户单据号全局唯一。
    /// </remarks>
    /// <example>
    /// 示例值: 89560002019101000121
    /// </example>
    [Required]
    [StringLength(128, MinimumLength = 1)]
    [JsonProperty("out_request_no")]
    public string OutRequestNo { get; set; }

    /// <summary>
    /// 扩展属性。
    /// </summary>
    /// <remarks>
    /// 扩展属性字段，按 json 格式，如无需要则不填写。<br/>
    /// 该字段暂未开放。
    /// </remarks>
    /// <example>示例值: {'exinfo1':'1234','exinfo2':'3456'}</example>
    [StringLength(128, MinimumLength = 1)]
    [JsonProperty("ext_info")]
    [CanBeNull]
    [JsonIgnore]
    [Obsolete("该字段暂未开放。")]
    public string ExtInfo { get; set; }

    public class InnerStockUseRule
    {
        /// <summary>
        /// 发放总上限。
        /// </summary>
        /// <remarks>
        /// 最大发券数<br/>
        /// 校验规则: <br/>
        /// 1. 发放总个数最少 5 个。
        /// 2. 发放总个数最多 1000 万个。
        /// </remarks>
        /// <example>示例值: 100</example>
        [Required]
        [JsonProperty("max_coupons")]
        public ulong MaxCoupons { get; set; }

        /// <summary>
        /// 总预算。
        /// </summary>
        /// <remarks>
        /// 最大发券预算，当营销经费 no_cash 选择预充值 false 时，激活批次时会从制券商户的余额中扣除预算，请保证账户金额充足，单位: 分<br/>
        /// max_amount 需要等于 coupon_amount(面额) * max_coupons(发放总上限)。<br/>
        /// 校验规则: 批次总预算最多 1000 万元。
        /// </remarks>
        /// <example>示例值: 5000</example>
        [Required]
        [JsonProperty("max_amount")]
        public ulong MaxAmount { get; set; }

        /// <summary>
        /// 单天预算发放上限。
        /// </summary>
        /// <remarks>
        /// 设置此字段，允许指定单天最大发券预算，单位: 分。<br/>
        /// 校验规则:<br/>
        /// 1. 不能大于总预算。<br/>
        /// 2. max_amount_by_day不可以为 0。
        /// </remarks>
        /// <example>示例值: 400</example>
        [JsonProperty("max_amount_by_day")]
        public ulong? MaxAmountByDay { get; set; }

        /// <summary>
        /// 活动期间每个用户可领个数，当开启了自然人限领时，多个微信号同属于一个身份证时，视为同一用户。<br/>
        /// 校验规则:<br/>
        /// 1: 不能大于发放总个数。<br/>
        /// 2: 最少为 1 个，最多为 60 个。
        /// </summary>
        /// <example>示例值: 3</example>
        [Required]
        [Range(1, 60)]
        [JsonProperty("max_coupons_per_user")]
        public uint MaxCouponsPerUser { get; set; }

        /// <summary>
        /// 是否开启自然人限制。
        /// </summary>
        /// <remarks>
        /// 当开启了自然人限领时，多个微信号同属于一个身份证时，视为同一用户，枚举值。<br/>
        /// true: 是<br/>
        /// false: 否
        /// </remarks>
        /// <example>示例值: false</example>
        [Required]
        [JsonProperty("natural_person_limit")]
        public bool NaturalPersonLimit { get; set; }

        /// <summary>
        /// 是否开启防刷拦截。
        /// </summary>
        /// <remarks>
        /// 若开启防刷拦截，当用户命中恶意、小号、机器、羊毛党、黑产等风险行为时，无法成功发放代金券。<br/>
        /// 枚举值<br/>
        /// true: 是<br/>
        /// false: 否
        /// </remarks>
        [Required]
        [JsonProperty("prevent_api_abuse")]
        public bool PreventApiAbuse { get; set; }
    }

    public class InnerPatternInfo
    {
        /// <summary>
        /// 使用说明。
        /// </summary>
        /// <remarks>
        /// 用于说明详细的活动规则,会展示在代金券详情页。<br/>
        /// 校验规则：最多 1000 个 UTF8 字符。
        /// </remarks>
        /// <example>示例值: 微信支付营销代金券</example>
        [Required]
        [StringLength(3000)]
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 商户 Logo。
        /// </summary>
        /// <remarks>
        /// 商户 Logo,仅支持通过 <a href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_0_1.shtml">图片上传 API</a> 接口获取的图片 URL 地址。<br/>
        /// 1. 商户 Logo大小需为 120 像素 * 120 像素。<br/> 
        /// 2. 支持 JPG/JPEG/PNG 格式,且图片小于 1M。<br/>
        /// 3. 最多 128 个 UTF8 字符。
        /// </remarks>
        /// <example>示例值: https://qpic.cn/xxx</example>
        [StringLength(128)]
        [JsonProperty("merchant_logo")]
        [CanBeNull]
        public string MerchantLogo { get; set; }

        /// <summary>
        /// 品牌名称。 
        /// </summary>
        /// <remarks>
        /// 品牌名称，展示在用户卡包。<br/>
        /// 校验规则:<br/>
        /// 1. 最多 12 个中文汉字。<br/>
        /// 2. 最多 36 个英文字符。
        /// </remarks>
        /// <example>示例值: 微信支付</example>
        [StringLength(128)]
        [JsonProperty("merchant_name")]
        [CanBeNull]
        public string MerchantName { get; set; }

        /// <summary>
        /// 背景颜色。
        /// </summary>
        /// <remarks>
        /// 券的背景颜色，可设置 10 种颜色，色值请参考 <a href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_1.shtml#menu1">卡券背景颜色图</a>。颜色取值为颜色图中的颜色名称。可选枚举字段不用则不传，不可以传空值。
        /// </remarks>
        /// <example>示例值: COLOR020</example>
        [StringLength(15)]
        [JsonProperty("background_color")]
        [CanBeNull]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 券详情图片。
        /// </summary>
        /// <remarks>
        /// 券详情图片， 850 像素 * 350 像素，且图片大小不超过 2M，支持 JPG/PNG 格式，仅支持通过 <a href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_0_1.shtml">图片上传 API</a> 接口获取的图片 URL 地址。
        /// </remarks> 
        /// <example>示例值: https://qpic.cn/xxx</example>
        [StringLength(128)]
        [JsonProperty("coupon_image")]
        [CanBeNull]
        public string CouponImage { get; set; }
    }

    public class InnerCouponUseRule
    {
        /// <summary>
        /// 券生效时间。
        /// </summary>
        /// <remarks>
        /// 允许指定券的特殊生效时间规则。<br/>
        /// 该字段暂未开放。
        /// </remarks>
        [JsonProperty("coupon_available_time")]
        [JsonIgnore]
        [Obsolete("该字段暂未开放。")]
        public InnerCouponAvailableTime CouponAvailableTime { get; set; }

        /// <summary>
        /// 固定面额满减券使用规则。
        /// </summary>
        /// <remarks>
        /// stock_type 为 NORMAL 时必填。
        /// </remarks>
        [JsonProperty("fixed_normal_coupon")]
        public InnerFixedNormalCoupon FixedNormalCoupon { get; set; }

        /// <summary>
        /// 订单优惠标记。
        /// </summary>
        /// <remarks>
        /// 订单优惠标记,按 JSON 格式。<br/>
        /// 商户下单时需要传入相同的标记(goods_tag),用户同时符合其他规则才能享受优惠<br/>
        /// 校验规则:<br/>
        /// 1. 最多允许录入 50 个。<br/>
        /// 2. 每个订单优惠标记支持字母/数字/下划线，不超过 128 个 UTF8 字符。
        /// </remarks>
        /// <example>示例值: ["123321","456654"]</example>
        [StringLength(128, MinimumLength = 1)]
        [MaxLength(50)]
        [JsonProperty("goods_tag")]
        public string[] GoodsTag { get; set; }

        /// <summary>
        /// 指定付款方式。
        /// </summary>
        /// <remarks>
        /// 指定付款方式的交易可核销/使用代金券,可指定零钱付款、指定银行卡付款,需填入 <a href="https://pay.weixin.qq.com/wiki/doc/apiv3/download/%E9%93%B6%E8%A1%8C%E5%8D%A1%E7%BC%96%E7%A0%81.xlsx">支付方式编码</a>,
        /// 不在此列表中的银行卡,暂不支持此功能。黄色标记部分为不可使用的银行。<br/>
        /// 校验规则: 条目个数限制为【1,1】。<br/>
        /// 零钱: CFT
        /// </remarks>
        /// <example>示例值: ICBC_CREDIT</example>
        [MaxLength(1)]
        [JsonProperty("limit_pay")]
        public string[] LimitPay { get; set; }

        /// <summary>
        /// 指定银行卡 BIN。
        /// </summary>
        /// <remarks>
        /// 指定银行卡 Bin 付款的交易可核销/使用代金券,当批次限定了指定银行卡时方可生效。
        /// </remarks>
        [JsonProperty("limit_card")]
        public InnerLimitCard LimitCard { get; set; }

        /// <summary>
        /// 支付方式。
        /// </summary>
        /// <remarks>
        /// 允许指定支付方式的交易才可核销/使用代金券,不填则默认“不限”。<br/>
        /// 枚举值:<br/>
        /// MICROAPP: 小程序支付 <br/>
        /// APPPAY: APP支付 <br/>
        /// PPAY: 免密支付 <br/>
        /// CARD: 刷卡支付 <br/>
        /// FACE: 人脸支付 <br/>
        /// OTHER: 其他支付
        /// </remarks>
        /// <example>示例值: ["MICROAPP","APPPAY"]</example>
        [JsonProperty("trade_type")]
        public string[] TradeType { get; set; }

        /// <summary>
        /// 是否可叠加其他优惠。
        /// </summary>
        /// <remarks>
        /// 允许指定本优惠是否可以和本商户号创建的其他券同时使用，不填则默认允许同时使用。枚举值:<br/>
        /// true: 是<br/>
        /// false: 否
        /// </remarks>
        /// <example>示例值: false</example>
        [JsonProperty("combine_use")]
        public bool? CombineUse { get; set; }

        /// <summary>
        /// 可核销商品编码。
        /// </summary>  
        /// <remarks>
        /// 包含指定 SKU 商品编码的交易才可核销/使用代金券：活动商户在交易下单时，需传入用户购买的所有 SKU 商品编码，当命中代金券中设置的商品编码时可享受优惠。<br/>
        /// 校验规则:<br/>
        /// 1. 单个商品编码的字符长度为【1,128】<br/>
        /// 2. 条目个数限制为【1,50】
        /// </remarks>
        /// <example>示例值: ['123321','456654']</example>
        [JsonProperty("available_items")]
        [MaxLength(50)]
        [StringLength(128, MinimumLength = 1)]
        public string[] AvailableItems { get; set; }

        /// <summary>
        /// 不可核销商品编码。
        /// </summary>
        /// <remarks>
        /// 包含指定SKU商品编码的交易不可核销/使用代金券。<br/>
        /// 校验规则: <br/>
        /// 1. 单个商品编码的字符长度为【1，128】<br/>
        /// 2. 条目个数限制为【1，50】
        /// </remarks>
        /// <example>示例值: ['789987','56765']</example>
        [Obsolete("该字段暂未开放。")]
        [JsonIgnore]
        [StringLength(128, MinimumLength = 1)]
        [MaxLength(50)]
        [JsonProperty("unavailable_items")]
        public string UnavailableItems { get; set; }

        /// <summary>
        /// 可用商户号。
        /// </summary>
        /// <remarks>
        /// 可用商户的交易才可核销/使用代金券。当营销经费 no_cash=false 时,可用商户允许填入任何类型的特约商户或普通商户 <br/>
        /// 当营销经费 no_cash=true 时,分为以下几种情况:<br/>
        /// 1. 创建商户是普通商户或服务商特约商户(子商户):可添加本商户号或同品牌商户。<br/>  
        /// <span class="red">说明:</span>若可用商户中,有特约商户(子商户),那么特约商户自己发起的交易、以及服务商帮特约商户发起的交易,都可以使用代金券。<br/>
        /// 2. 创建商户是普通服务商:可添加已授权的子商户,详见《申请免充值代金券产品权限》。<br/>
        /// <span class="red">说明:</span>特约商户如果有多个服务商,那么服务商为他发起的交易,只要完成了免充值授权,都可以使用代金券;特约商户自己发起的交易不可以使用代金券。<br/>
        /// 3. 创建商户是渠道商、银行服务商或从业机构:可直接添加旗下任意子商户,不需要子商户授权。<br/>
        /// 校验规则:条目个数限制为【1,50】  
        /// </remarks>
        /// <example>示例值: ['9856000','9856111']</example>
        [MaxLength(50)]
        [JsonProperty("available_merchants")]
        public string[] AvailableMerchants { get; set; }

        /// <summary>
        /// 指定银行卡 BIN。
        /// </summary>
        public class InnerLimitCard
        {
            /// <summary>
            /// 银行卡名称。
            /// </summary>
            /// <remarks>
            /// 将在微信支付收银台向用户展示,最多 4 个中文汉字。
            /// </remarks>
            /// <example>示例值: 精粹白金</example>
            [StringLength(4)]
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// 指定卡 BIN。
            /// </summary>
            /// <remarks>
            /// 使用指定卡 BIN 的银行卡支付方可享受优惠,按 JSON 格式<br/>
            /// 特殊规则:单个卡BIN的字符长度为【6,9】,条目个数限制为【1,10】。
            /// </remarks>
            /// <example>示例值: ['62123456','62123457']</example>
            [MaxLength(10)]
            [StringLength(9, MinimumLength = 6)]
            [JsonProperty("bin")]
            public string[] Bin { get; set; }
        }

        /// <summary>
        /// 固定面额满减券使用规则。
        /// </summary>
        public class InnerFixedNormalCoupon
        {
            /// <summary>
            /// 面额。
            /// </summary>
            /// <remarks>
            /// 面额,单位: 分。<br/>
            /// 校验规则:<br/>
            /// 1. 必须为整数。<br/>
            /// 2. 必须大于 1 元且小于等于 500 元。
            /// </remarks>
            /// <example>示例值: 100。</example>
            [JsonProperty("coupon_amount")]
            public ulong CouponAmount { get; set; }

            /// <summary>
            /// 门槛。
            /// </summary>
            /// <remarks>
            /// 使用券金额门槛,单位: 分。<br/>
            /// 若指定可核销商品编码,门槛则为可核销商品部分的消费金额,而不是订单的消费金额。<br/>
            /// 校验规则: 使用门槛必须大于优惠金额。  
            /// </remarks>
            /// <example>示例值: 100</example>
            [JsonProperty("transaction_minimum")]
            public ulong TransactionMinimum { get; set; }
        }

        /// <summary>
        /// 券生效时间。
        /// </summary>
        public class InnerCouponAvailableTime
        {
            /// <summary>
            /// 固定时间段可用。
            /// </summary>
            [Obsolete("该字段暂未开放。")]
            [JsonIgnore]
            [JsonProperty("fix_available_time")]
            public InnerFixAvailableTime FixAvailableTime { get; set; }

            /// <summary>
            /// 领取后 N 天有效。
            /// </summary>
            /// <remarks>
            /// 领取后,券的开始时间为领券后第二天,如 7 月 1 日领券,那么在 7 月 2 日 00:00:00 开始。<br/>
            /// 当设置领取后 N 天有效时,不可设置固定时间段可用。<br/> 
            /// 枚举值:<br/>
            /// true: 是<br/>
            /// false: 否
            /// </remarks>
            /// <example>示例值: false</example>
            [Obsolete("该字段暂未开放。")]
            [JsonIgnore]
            [JsonProperty("second_day_available")]
            public bool? SecondDayAvailable { get; set; }

            /// <summary>
            /// 领取后有效时间。
            /// </summary>
            /// <remarks>
            /// 领取后,券的结束时间为领取 N 天后,如设置领取后 7 天有效,那么 7 月 1 日领券,在 7 月 7 日 23:59:59 失效(在可用时间内计算失效时间,若券还未到领取后 N 天,但是已经到了可用结束时间,那么也会过期)<br/>
            /// 领取后有效时间,单位: 分钟。<br/>
            /// 该字段暂未开放。
            /// </remarks>
            /// <example>示例值: 1440</example>
            [Obsolete("该字段暂未开放。")]
            [JsonIgnore]
            [JsonProperty("available_time_after_receive")]
            public uint? AvailableTimeAfterReceive { get; set; }
        }

        /// <summary>
        /// 固定时间段可用。
        /// </summary>
        public class InnerFixAvailableTime
        {
            /// <summary>
            /// 可用星期数。
            /// </summary>
            /// <remarks>
            /// 允许指定每周固定星期数生效,0 代表周日生效, 1 代表周一生效,以此类推;不填则代表在可用时间内周一至周日都生效。<br/>
            /// </remarks>
            /// <example>示例值: 1,2</example>
            [Obsolete("该字段暂未开放。")]
            [JsonProperty("available_week_day")]
            [JsonIgnore]
            public uint[] AvailableWeekDay { get; set; }

            /// <summary>
            /// 当天开始时间。
            /// </summary>
            /// <remarks>
            /// 允许指定特殊生效星期数中的具体生效的时间段。<br/>
            /// 当天开始时间,单位: 秒。<br/>
            /// </remarks>
            /// <example>示例值: 0</example>
            [Obsolete("该字段暂未开放。")]
            [JsonProperty("begin_time")]
            [JsonIgnore]
            public uint? BeginTime { get; set; }

            /// <summary>
            /// 当天结束时间。
            /// </summary>
            /// <remarks>
            /// 允许指定特殊生效星期数中的具体生效的时间段。<br/>
            /// 当天结束时间,单位:秒,默认为 23 点 59 分 59 秒。<br/>
            /// </remarks>
            /// <example>示例值: 3600</example>
            [Obsolete("该字段暂未开放。")]
            [JsonProperty("end_time")]
            [JsonIgnore]
            public uint? EndTime { get; set; }
        }
    }
}