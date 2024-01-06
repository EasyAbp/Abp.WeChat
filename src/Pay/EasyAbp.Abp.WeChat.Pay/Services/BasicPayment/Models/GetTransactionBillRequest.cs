using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class GetTransactionBillRequest
{
    /// <summary>
    /// 账单日期。
    /// </summary>
    /// <remarks>
    /// 格式 yyyy-MM-dd。 <br/>
    /// 仅支持三个月内的账单下载申请。
    /// </remarks>
    /// <example>
    /// 示例值: 2019-06-11
    /// </example>
    [Required]
    [StringLength(10, MinimumLength = 1)]
    [JsonProperty("bill_date")]
    public string BillDate { get; set; }

    /// <summary>
    /// 账单类型。
    /// </summary>
    /// <remarks>
    /// 枚举值，具体定义参考类型 <see cref="BillTypeEnum"/>，不填则默认是 ALL。
    /// </remarks>
    /// <example>
    /// 示例值: ALL (<see cref="BillTypeEnum.All"/>)
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("bill_type")]
    public string BillType { get; set; }

    /// <summary>
    /// 压缩类型。
    /// </summary>
    /// <remarks>
    /// 不填则默认是数据流。<br/>
    /// GZIP: 返回格式为.gzip的压缩包账单。
    /// </remarks>
    /// <example>
    /// 示例值: GZIP
    /// </example>
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("tar_type")]
    public string TarType { get; set; }
}