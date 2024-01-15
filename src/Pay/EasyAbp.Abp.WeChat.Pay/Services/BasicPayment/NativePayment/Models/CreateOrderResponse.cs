using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.NativePayment.Models;

/// <summary>
/// Native 支付 - 统一下单 - 返回结果。
/// </summary>
public class CreateOrderResponse : WeChatPayCommonErrorResponse
{
    /// <summary>
    /// 二维码链接。
    /// </summary>
    /// <remarks>
    /// 此URL用于生成支付二维码，然后提供给用户扫码支付。
    /// 注意: code_url 并非固定值，使用时按照 URL 格式转成二维码即可。
    /// </remarks>
    /// <example>
    /// 示例值: weixin://wxpay/bizpayurl/up?pr=NwY5Mz9&groupid=00
    /// </example>
    [JsonProperty("code_url")]
    public string CodeUrl { get; set; }
}