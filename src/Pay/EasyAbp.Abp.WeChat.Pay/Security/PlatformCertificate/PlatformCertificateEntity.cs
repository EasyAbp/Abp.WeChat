using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

[Serializable]
public class PlatformCertificateEntity
{
    public string SerialNo { get; set; }

    public string CertificateString { get; set; }

    public DateTime EffectiveTime { get; set; }

    public DateTime ExpireTime { get; set; }

    public X509Certificate2 Certificate { get; set; }

    public PlatformCertificateEntity()
    {
    }

    public PlatformCertificateEntity(
        string serialNo, string certificateString, DateTime effectiveTime, DateTime expireTime)
    {
        SerialNo = serialNo;
        CertificateString = certificateString;
        EffectiveTime = effectiveTime;
        ExpireTime = expireTime;
        Certificate = new X509Certificate2(Encoding.UTF8.GetBytes(CertificateString));
    }

    public bool VerifySignature(string message, string signature)
    {
        var rsa = Certificate.GetRSAPublicKey();
        var data = Encoding.UTF8.GetBytes(message);
        var sign = Convert.FromBase64String(signature);
        return rsa.VerifyData(data, sign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    public string Encrypt(string message)
    {
        var rsa = Certificate.GetRSAPublicKey();
        var data = Encoding.UTF8.GetBytes(message);
        var encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        return Convert.ToBase64String(encryptedData);
    }
}