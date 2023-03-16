using System;
using System.Net.Http;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public class HttpMessageHandlerCacheModel
{
    public HttpMessageHandler Handler { get; set; }

    public byte[] CertificateBytes { get; set; }

    public string CertificateSecret { get; set; }

    public DateTime SkipCertificateBytesCheckUntil { get; set; }
}