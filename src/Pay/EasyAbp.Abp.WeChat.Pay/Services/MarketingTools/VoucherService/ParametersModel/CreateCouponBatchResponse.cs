using System;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService.ParametersModel;

public class CreateCouponBatchResponse : WeChatPayCommonErrorResponse
{
    /// <summary>
    /// 批次号。
    /// </summary>
    /// <remarks>
    /// 微信为每个代金券批次分配的唯一 ID。
    /// </remarks>
    /// <example>示例值: 98065001</example>
    [JsonProperty("stock_id")]
    public string StockId { get; set; }

    /// <summary>
    /// 创建时间。
    /// </summary>
    /// <remarks>
    /// 创建时间，遵循 <a href="https://datatracker.ietf.org/doc/html/rfc3339">rfc3339</a> 标准格式，格式为 yyyy-MM-DDTHH:mm:ss+TIMEZONE，
    /// yyyy-MM-DD 表示年月日，T 出现在字符串中，表示 time 元素的开头，HH:mm:ss 表示时分秒，TIMEZONE 表示时区(+08:00 表示东八区时间，领先 UTC 8小时，即北京时间)。
    /// 例如: 2015-05-20T13:29:35+08:00 表示，北京时间 2015 年 5 月 20 日 13 点 29 分 35 秒。
    /// </remarks>
    /// <example>示例值: 2015-05-20T13:29:35.+08:00</example>
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
}