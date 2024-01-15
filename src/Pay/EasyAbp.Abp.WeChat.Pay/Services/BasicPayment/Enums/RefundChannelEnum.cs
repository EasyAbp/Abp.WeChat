namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 退款渠道枚举。
/// </summary>
public static class RefundChannelEnum
{
    /// <summary>
    /// 原路退款。
    /// </summary>
    public const string Original = "ORIGINAL";

    /// <summary>
    /// 退回到余额。
    /// </summary>
    public const string Balance = "BALANCE";

    /// <summary>
    /// 原账户异常退到其他余额账户。
    /// </summary>
    public const string OtherBalance = "OTHER_BALANCE";

    /// <summary>
    /// 原银行卡异常退到其他银行卡。
    /// </summary>
    public const string OtherBankCard = "OTHER_BANKCARD";
}