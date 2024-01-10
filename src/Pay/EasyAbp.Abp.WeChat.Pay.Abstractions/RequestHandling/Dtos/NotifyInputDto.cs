using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class NotifyInputDto
{
    [CanBeNull] public string MchId { get; set; }

    public string RequestBodyString { get; set; }

    public WeChatPayNotificationInput RequestBody { get; set; }

    public NotifyHttpHeaderModel HttpHeader { get; set; }
}