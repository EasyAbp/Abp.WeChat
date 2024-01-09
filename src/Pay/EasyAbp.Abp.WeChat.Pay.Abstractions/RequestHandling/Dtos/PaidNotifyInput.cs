using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class PaidNotifyInput
{
    [CanBeNull] public string MchId { get; set; }

    public PaymentNotifyCallbackRequest RequestBody { get; set; }

    public string SerialNumber { get; set; }

    public string Timestamp { get; set; }

    public string Nonce { get; set; }

    public string RequestBodyString { get; set; }

    public string Signature { get; set; }
}