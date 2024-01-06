using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class GetFundFlowBillRequest
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
    /// 资金账户类型。
    /// </summary>
    /// <remarks>
    /// 枚举值，具体定义参考类型 <see cref="FundAccountTypeEnum"/>，不填则默认是 BASIC。
    /// </remarks>
    /// <example>
    /// 示例值: Basic (<see cref="FundAccountTypeEnum.Basic"/>)
    /// </example>
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("account_type")]
    public string AccountType { get; set; }

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