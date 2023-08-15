using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.ParametersModel;

public class CreateOrderResponse
{
    /// <summary>
    /// 预支付交易会话标识。
    /// </summary>
    /// <remarks>
    /// 预支付交易会话标识。用于后续接口调用中使用，该值有效期为 2 小时。
    /// </remarks>
    /// <example>
    /// 示例值: wx201410272009395522657a690389285100
    /// </example>
    [JsonProperty("prepay_id")]
    public string PrepayId { get; set; }
}