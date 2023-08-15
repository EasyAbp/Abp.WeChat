using System.Security.Cryptography.X509Certificates;
using EasyAbp.Abp.WeChat.Common.Extensions;

namespace EasyAbp.Abp.WeChat.Pay.Security;

/// <summary>
/// 微信支付的证书实体，包含了证书的内容和证书的哈希值。
/// </summary>
public sealed class WeChatPayCertificate
{
    /// <summary>
    /// 商户号。
    /// </summary>
    public string MchId { get; set; }

    /// <summary>
    /// X509 证书实例。
    /// </summary>
    public X509Certificate2 X509Certificate { get; set; }

    /// <summary>
    /// X509 证书的哈希值，用于快速比对证书是否发生变化。
    /// </summary>
    public byte[] CertificateHashCode { get; set; }

    /// <summary>
    /// 构建一个新的 <see cref="WeChatPayCertificate"/> 实例。
    /// </summary>
    /// <param name="mchId">商户号。</param>
    /// <param name="certificateBytes">X509 证书实例。</param>
    /// <param name="password">X509 证书的哈希值，用于快速比对证书是否发生变化。</param>
    public WeChatPayCertificate(string mchId, byte[] certificateBytes, string password)
    {
        MchId = mchId;
        X509Certificate = new X509Certificate2(certificateBytes,
            password,
            X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
        CertificateHashCode = certificateBytes.Sha256();
    }
}