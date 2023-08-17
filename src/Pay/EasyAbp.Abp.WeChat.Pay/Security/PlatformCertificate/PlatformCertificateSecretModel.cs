using System;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public class PlatformCertificateSecretModel
{
    public string SerialNo { get; set; }

    public string Certificate { get; set; }

    public DateTime EffectiveTime { get; set; }

    public DateTime ExpireTime { get; set; }

    public PlatformCertificateSecretModel()
    {
    }

    public PlatformCertificateSecretModel(
        string serialNo, string certificate, DateTime effectiveTime, DateTime expireTime)
    {
        SerialNo = serialNo;
        Certificate = certificate;
        EffectiveTime = effectiveTime;
        ExpireTime = expireTime;
    }
}