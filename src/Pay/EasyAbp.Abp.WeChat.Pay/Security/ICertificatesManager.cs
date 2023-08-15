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
}