namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 账单类型枚举。
/// </summary>
public static class TransactionBillTypeEnum
{
    /// <summary>
    /// 回当日所有订单信息 (不含充值退款订单)。
    /// </summary>
    public const string All = "ALL";

    /// <summary>
    /// 返回当日成功支付的订单 (不含充值退款订单)。
    /// </summary>
    public const string Success = "SUCCESS";

    /// <summary>
    /// 返回当日退款订单 (不含充值退款订单)。
    /// </summary>
    public const string Refund = "REFUND";
}