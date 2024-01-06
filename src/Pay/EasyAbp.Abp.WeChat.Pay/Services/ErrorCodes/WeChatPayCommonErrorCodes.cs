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