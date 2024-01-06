namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

/// <summary>
/// 优惠类型的枚举值定义。
/// </summary>
public static class PromotionTypeEnum
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