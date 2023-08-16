using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;

public class CreateOrderRequest : BasicPayment.Models.CreateOrderRequest
{
    /// <summary>
    /// 支付者信息。
    /// </summary>
    [Required]
    [NotNull]
    [JsonProperty("payer")]
    public CreateOrderPayerModel Payer { get; set; }
}