namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 交易状态的枚举值定义。
/// </summary>
public static class TradeStateEnum
{
    /// <summary>
    /// 支付成功。
    /// </summary>
    public const string Success = "SUCCESS";

    /// <summary>
    /// 转入退款。
    /// </summary>
    public const string Refund = "REFUND";

    /// <summary>
    /// 未支付。
    /// </summary>
    public const string NotPay = "NOTPAY";

    /// <summary>
    /// 已关闭。
    /// </summary>
    public const string Closed = "CLOSED";

    /// <summary>
    /// 已撤销 (仅付款码支付会返回)。
    /// </summary>
    public const string Revoked = "REVOKED";

    /// <summary>
    /// 用户支付中 (仅付款码支付会返回)。
    /// </summary>
    public const string UserPaying = "USERPAYING";

    /// <summary>
    /// 支付失败 (仅付款码支付会返回)。
    /// </summary>
    public const string PayError = "PAYERROR";
}