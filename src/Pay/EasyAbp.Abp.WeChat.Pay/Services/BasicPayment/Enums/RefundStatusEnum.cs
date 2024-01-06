namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Enums;

/// <summary>
/// 退款状态枚举。
/// </summary>
public static class RefundStatusEnum
{
    /// <summary>
    /// 退款成功。
    /// </summary>
    public const string Success = "SUCCESS";
    
    /// <summary>
    /// 退款关闭。
    /// </summary>
    public const string Closed = "CLOSED";
    
    /// <summary>
    /// 退款处理中。
    /// </summary>
    public const string Processing = "PROCESSING";
    
    /// <summary>
    /// 退款异常。
    /// </summary>
    public const string Abnormal = "ABNORMAL";
}