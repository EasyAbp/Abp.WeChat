using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Security;

/// <summary>
/// <see cref="ICertificatesManager"/> 的默认实现。
/// </summary>
public class CertificatesManager : ICertificatesManager, ITransientDependency
{
    protected IAbpLazyServiceProvider AbpLazyServiceProvider { get; }
    protected IAbpWeChatPayOptionsProvider AbpWeChatPayOptionsProvider { get; }

    protected ConcurrentDictionary<string, Lazy<WeChatPayCertificate>> CertificatesCache { get; } = new();

    public const string DefaultCertificateKey = "__Default__";

    public CertificatesManager(IAbpLazyServiceProvider abpLazyServiceProvider,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider)
    {
        AbpLazyServiceProvider = abpLazyServiceProvider;
        AbpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
    }

    public async Task<WeChatPayCertificate> GetCertificateAsync(string mchId)
    {
        var options = await AbpWeChatPayOptionsProvider.GetAsync(mchId);
        var certificateBytes = await GetX509CertificateBytesAsync(options);

        return CertificatesCache.GetOrAdd(mchId ?? DefaultCertificateKey, _ =>
            new Lazy<WeChatPayCertificate>(() =>
                new WeChatPayCertificate(options.MchId, certificateBytes, options.CertificateSecret))).Value;
    }

    protected virtual async Task<byte[]> GetX509CertificateBytesAsync(AbpWeChatPayOptions options)
    {
        if (options.CertificateBlobName.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(options.CertificateBlobName), "证书的 BLOB 名称为必选项。");
        }

        var blobContainer = options.CertificateBlobContainerName.IsNullOrWhiteSpace()
            ? AbpLazyServiceProvider.LazyGetRequiredService<IBlobContainer>()
            : AbpLazyServiceProvider.LazyGetRequiredService<IBlobContainerFactory>()
                .Create(options.CertificateBlobContainerName);

        var certificateBytes = await blobContainer.GetAllBytesAsync(options.CertificateBlobName);
        if (certificateBytes == null)
        {
            throw new NullReferenceException("无法获取微信商户证书，请检查对应的 BLOB 是否存在。");
        }

        return certificateBytes;
    }
}