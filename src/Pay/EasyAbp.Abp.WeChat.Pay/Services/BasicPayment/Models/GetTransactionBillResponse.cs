using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public class GetTransactionBillResponse : WeChatPayCommonErrorResponse
{
    /// <summary>
    /// 哈希类型。
    /// </summary>
    /// <remarks>
    /// 枚举值: <br/>
    /// SHA1: SHA1 值
    /// </remarks>
    /// <example>
    /// 示例值: SHA1
    /// </example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    [JsonProperty("hash_type")]
    public string HashType { get; set; }

    /// <summary>
    /// 哈希值。
    /// </summary>
    /// <remarks>
    /// 原始账单 (gzip 需要解压缩) 的摘要值，用于校验文件的完整性。
    /// </remarks>
    /// <example>
    /// 示例值: 79bb0f45fc4c42234a918000b2668d689e2bde04
    /// </example>
    [Required]
    [StringLength(1024, MinimumLength = 1)]
    [JsonProperty("hash_value")]
    public string HashValue { get; set; }

    /// <summary>
    /// 账单下载地址。
    /// </summary>
    /// <remarks>
    /// 供下一步请求账单文件的下载地址，该地址 30s 内有效。
    /// </remarks>
    /// <example>
    /// 示例值: https://api.mch.weixin.qq.com/v3/billdownload/file?token=xxx
    /// </example>
    [Required]
    [StringLength(2048, MinimumLength = 1)]
    [JsonProperty("download_url")]
    public string DownloadUrl { get; set; }
}