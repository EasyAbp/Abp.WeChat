using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Pay.Security;

/// <summary>
/// 证书管理器，用于管理每个商户号对应的微信支付证书(X509)。
/// </summary>
public interface ICertificatesManager
{
    /// <summary>
    /// 使用商户号<paramref name="mchId"/> 获取对应的微信支付证书(X509)。
    /// </summary>
    /// <param name="mchId">微信支付商户号。</param>
    /// <returns>取得的商户证书信息，当证书不存在的时候会抛出异常信息，请注意处理。</returns>
    /// <exception cref="ArgumentNullException">当证书的 BLOB 不存在时抛出此异常。</exception>
    /// <exception cref="NullReferenceException">无法获取证书时会抛出此异常。</exception>
    Task<WeChatPayCertificate> GetCertificateAsync(string mchId);

    /// <summary>
    /// 使用微信支付证书对待签名字符串进行签名。
    /// </summary>
    /// <param name="pendingSignature">等待签名的字符串。</param>
    /// <param name="certificate">用于签名的证书实例。</param>
    /// <returns>具体的签名数据，使用 Base64 编码。</returns>
    string GetSignature(string pendingSignature, WeChatPayCertificate certificate);
}