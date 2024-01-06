namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

/// <summary>
/// 资金账户类型枚举。
/// </summary>
public static class FundAccountTypeEnum
{
    /// <summary>
    /// 基本账户。
    /// </summary>
    public const string Basic = "BASIC";

    /// <summary>
    /// 运营账户。
    /// </summary>
    public const string Operation = "OPERATION";

    /// <summary>
    /// 手续费账户。
    /// </summary>
    public const string Fees = "FEES";
}