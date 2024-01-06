namespace EasyAbp.Abp.WeChat.Pay.Services.ErrorCodes;

public class BasicPaymentErrorCode : WeChatPayCommonErrorCodes
{
    /// <summary>
    /// 描述: 账号异常。
    /// 解决方案: 用户账号异常，无需更多操作。
    /// </summary>
    public const string AccountError = "ACCOUNT_ERROR";

    /// <summary>
    /// 描述: 业务规则限制。
    /// 解决方案: 因业务规则限制请求频率，请查看接口返回的详细信息。
    /// </summary>
    public const string RuleLimit = "RULE_LIMIT";

    /// <summary>
    /// 描述: 余额不足。
    /// 解决方案: 用户账号余额不足，请用户充值或更换支付卡后再支付。
    /// </summary>
    public const string NotEnough = "NOT_ENOUGH";

    /// <summary>
    /// 描述: 商户无权限。
    /// 解决方案: 请商户前往申请此接口相关权限。
    /// </summary>
    public const string NoAuth = "NO_AUTH";

    /// <summary>
    /// 描述: 系统错误。
    /// 解决方案: 系统异常，请用相同参数重新调用。
    /// </summary>
    public const string SystemError = "SYSTEM_ERROR";

    /// <summary>
    /// 描述: OpenId 和 AppId 不匹配。
    /// 解决方案: 请确认 OpenId 和 AppId 是否匹配。
    /// </summary>
    public const string OpenIdMismatch = "OPENID_MISMATCH";

    /// <summary>
    /// 描述: 订单号非法。
    /// 解决方案: 请检查微信支付订单号是否正确。
    /// </summary>
    public const string InvalidTransactionId = "INVALID_TRANSACTIONID";

    /// <summary>
    /// 描述: 银行系统异常。
    /// 解决方案: 银行系统异常，请用相同参数重新调用。
    /// </summary>
    public const string BankError = "BANK_ERROR";
}