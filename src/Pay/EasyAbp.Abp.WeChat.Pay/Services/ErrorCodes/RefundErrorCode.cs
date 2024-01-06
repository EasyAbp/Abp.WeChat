namespace EasyAbp.Abp.WeChat.Pay.Services.ErrorCodes;

public class RefundErrorCode : BasicPaymentErrorCode
{
    /// <summary>
    /// 描述: 退款请求失败。
    /// 解决方案: 此状态代表退款申请失败，商户可自行处理退款。
    /// </summary>
    public const string UserAccountAbnormal = "USER_ACCOUNT_ABNORMAL";

    /// <summary>
    /// 描述: 订单号不存在。
    /// 解决方案: 请检查你的订单号是否正确且是否已支付，未支付的订单不能发起退款。
    /// </summary>
    public const string ResourceNotExists = "RESOURCE_NOT_EXISTS";
}