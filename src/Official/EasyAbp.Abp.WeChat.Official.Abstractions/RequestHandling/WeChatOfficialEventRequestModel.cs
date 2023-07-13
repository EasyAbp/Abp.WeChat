using System;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

[Serializable]
public class WeChatOfficialEventRequestModel : WeChatEventRequestModel
{
    /// <summary>
    /// 用于微信公众号验证
    /// </summary>
    public string EchoStr { get; set; }
}