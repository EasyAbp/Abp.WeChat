using System;
using System.Net.Http;
using EasyAbp.Abp.WeChat.Pay.Security;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public class HttpMessageHandlerCacheModel
{
    public HttpMessageHandler Handler { get; set; }

    public WeChatPayCertificate WeChatPayCertificate { get; set; }

    public DateTime SkipCertificateBytesCheckUntil { get; set; }
}