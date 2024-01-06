namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 交易类型的枚举值定义。
/// </summary>
public static class TradeTypeEnum
{
    /// <summary>
    /// 公众号支付。
    /// </summary>
    public const string JsApi = "JSAPI";

    /// <summary>
    /// 扫码支付。
    /// </summary>
    public const string Native = "NATIVE";

    /// <summary>
    /// APP 支付。
    /// </summary>
    public const string App = "APP";

    /// <summary>
    /// 付款码支付。
    /// </summary>
    public const string MicroPay = "MICROPAY";

    /// <summary>
    /// H5 支付。
    /// </summary>
    public const string MWeb = "MWEB";

    /// <summary>
    /// 刷脸支付。
    /// </summary>
    public const string FacePay = "FACEPAY";
}