namespace EasyAbp.Abp.WeChat.Pay.Services.ErrorCodes;

public class WeChatPayCommonErrorCodes
{
    /// <summary>
    /// 描述: 无效请求。
    /// 解决方案: 请检查请求参数是否正确。
    /// </summary>
    public const string InvalidRequest = "INVALID_REQUEST";

    /// <summary>
    /// 描述: AppId 和 MchId不匹配。
    /// 解决方案: 请检查 AppId 和 MchId 是否匹配。	
    /// </summary>
    public const string AppIdMchIdNotMatch = "APPID_MCHID_NOT_MATCH";

    /// <summary>
    /// 描述: 参数错误。
    /// 解决方案: 请根据接口返回的详细信息检查请求参数。
    /// </summary>
    public const string ParameterError = "PARAM_ERROR";

    /// <summary>
    /// 描述: 订单已关闭。
    /// 解决方案: 当前订单已关闭，请重新下单。
    /// </summary>
    public const string OrderClosed = "ORDER_CLOSED";

    /// <summary>
    /// 描述: 商户号不存在。
    /// 解决方案: 请检查商户号是否正确。
    /// </summary>
    public const string MchNotExists = "MCH_NOT_EXISTS";

    /// <summary>
    /// 描述: 签名错误。
    /// 解决方案: 请检查签名参数和方法是否都符合签名算法要求。
    /// </summary>
    public const string SignError = "SIGN_ERROR";

    /// <summary>
    /// 描述: 商户订单号重复。
    /// 解决方案: 请核实商户订单号是否重复提交。
    /// </summary>
    public const string OutTradeNoUsed = "OUT_TRADE_NO_USED";

    /// <summary>
    /// 描述: 交易错误。
    /// 解决方案: 因业务原因交易失败，请查看接口返回的详细信息。
    /// </summary>
    public const string TradeError = "TRADE_ERROR";

    /// <summary>
    /// 描述: 订单不存在。
    /// 解决方案: 请检查订单是否发起过交易。
    /// </summary>
    public const string OrderNotExist = "ORDER_NOT_EXIST";

    /// <summary>
    /// 描述: 订单不存在。
    /// 解决方案: 请检查订单是否发起过交易。
    /// </summary>
    public const string OrderNotExist2 = "ORDERNOTEXIST";

    /// <summary>
    /// 描述: 频率超限。
    /// 解决方案: 请降低请求接口频率。
    /// </summary>
    public const string FrequencyLimit = "FREQUENCY_LIMITED";
}

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
    public const string NotEnough = "NOTENOUGH";

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