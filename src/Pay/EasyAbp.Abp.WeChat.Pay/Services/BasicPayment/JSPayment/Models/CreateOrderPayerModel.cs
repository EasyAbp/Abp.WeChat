using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;

public class CreateOrderPayerModel
{
    /// <summary>
    /// 用户标识。
    /// </summary>
    /// <remarks>
    /// 用户在直连商户 AppId 下的唯一标识。<br/>
    /// 下单前需获取到用户的 OpenId。
    /// </remarks>
    /// <example>
    /// 示例值: oUpF8uMuAJO_M2pxb1Q9zNjWeS6o。
    /// </example>
    [Required]
    [StringLength(128, MinimumLength = 1)]
    [JsonProperty("openid")]
    public string OpenId { get; set; }
}