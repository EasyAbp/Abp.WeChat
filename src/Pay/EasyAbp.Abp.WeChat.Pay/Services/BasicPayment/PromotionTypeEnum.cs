namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

/// <summary>
/// 优惠类型的枚举值定义。
/// </summary>
public static class PromotionTypeEnum
{
    /// <summary>
    /// 代金券，需要走结算资金的充值型代金券。
    /// </summary>
    public const string Coupon = "COUPON";
    
    /// <summary>
    /// 优惠券，不走结算资金的免充值型优惠券。
    /// </summary>
    public const string Discount = "DISCOUNT";
}