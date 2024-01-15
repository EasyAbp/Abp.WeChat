namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 退款资金来源枚举。
/// </summary>
public static class RefundFundsAccountEnum
{
    /// <summary>
    /// 可用余额账户。
    /// </summary>
    public const string Available = "AVAILABLE";

    /// <summary>
    /// 不可用余额账户。
    /// </summary>
    public const string Unavailable = "UNAVAILABLE";

    /// <summary>
    /// 未结算资金账户。
    /// </summary>
    public const string Unsettled = "UNSETTLED";

    /// <summary>
    /// 运营账户。
    /// </summary>
    public const string Operation = "OPERATION";

    /// <summary>
    /// 基本账户。
    /// </summary>
    public const string Basic = "BASIC";
}