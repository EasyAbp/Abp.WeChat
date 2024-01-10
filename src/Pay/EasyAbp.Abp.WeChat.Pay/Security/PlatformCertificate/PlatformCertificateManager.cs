using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public class PlatformCertificateManager : IPlatformCertificateManager, ISingletonDependency
{
    public static string PlatformCertificatesCacheItemKey { get; set; } = nameof(PlatformCertificatesCacheItemKey);

    private readonly ILogger<PlatformCertificateManager> _logger;
    private readonly IAbpWeChatPayServiceFactory _abpWeChatPayServiceFactory;
    private readonly IAbpWeChatPayOptionsProvider _abpWeChatPayOptionsProvider;
    private readonly ConcurrentDictionary<string, Lazy<PlatformCertificateEntity>> _certificatesCache = new();

    public PlatformCertificateManager(
        ILogger<PlatformCertificateManager> logger,
        IAbpWeChatPayServiceFactory abpWeChatPayServiceFactory,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider)
    {
        _logger = logger;
        _abpWeChatPayServiceFactory = abpWeChatPayServiceFactory;
        _abpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
    }

    public virtual async Task<PlatformCertificateEntity> GetPlatformCertificateAsync(string mchId, string serialNo)
    {
        Check.NotNullOrWhiteSpace(mchId, nameof(mchId));
        Check.NotNullOrWhiteSpace(serialNo, nameof(serialNo));

        var cacheItem = _certificatesCache.TryGetValue(serialNo, out var lazyCertificate)
            ? lazyCertificate.Value
            : null;
        if (cacheItem != null) return cacheItem;

        var options = await _abpWeChatPayOptionsProvider.GetAsync(mchId);
        var certificateService = await _abpWeChatPayServiceFactory.CreateAsync<WeChatPayCertificatesWeService>(mchId);

        try
        {
            var certificates = await certificateService.GetPlatformCertificatesAsync();

            foreach (var certificate in certificates.Data)
            {
                var certificateString = WeChatPaySecurityUtility.AesGcmDecrypt(options.ApiV3Key,
                    certificate.EncryptCertificateData.AssociatedData,
                    certificate.EncryptCertificateData.Nonce,
                    certificate.EncryptCertificateData.Ciphertext);

                _certificatesCache.TryAdd(certificate.SerialNo,new Lazy<PlatformCertificateEntity>(() =>
                    new PlatformCertificateEntity(certificate.SerialNo, certificateString,
                        certificate.EffectiveTime, certificate.ExpireTime)));
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning("Fail to get and cache the platform certificates");
            _logger.LogException(e);
        }

        return _certificatesCache[serialNo].Value;
    }
}