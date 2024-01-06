namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 代金券的枚举值定义。
/// </summary>
public static class CouponTypeEnum
{
    /// <summary>
    /// 充值型代金券。
    /// </summary>
    public const string Cash = "CASH";

    /// <summary>
    /// 免充值型代金券。
    /// </summary>
    public const string NoCash = "NOCASH";
}