using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public class PlatformCertificateManager : IPlatformCertificateManager
{
    public static string PlatformCertificatesCacheItemKey { get; set; } = "empty";

    private const string LockName = "PlatformCertificateManager";

    private readonly ILogger<PlatformCertificateManager> _logger;
    private readonly IAbpWeChatPayServiceFactory _abpWeChatPayServiceFactory;
    private readonly IAbpWeChatPayOptionsProvider _abpWeChatPayOptionsProvider;
    private readonly IDistributedCache<PlatformCertificatesCacheItem> _distributedCache;
    private readonly IAbpDistributedLock _distributedLock;

    public PlatformCertificateManager(
        ILogger<PlatformCertificateManager> logger,
        IAbpWeChatPayServiceFactory abpWeChatPayServiceFactory,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider,
        IDistributedCache<PlatformCertificatesCacheItem> distributedCache,
        IAbpDistributedLock distributedLock)
    {
        _logger = logger;
        _abpWeChatPayServiceFactory = abpWeChatPayServiceFactory;
        _abpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
        _distributedCache = distributedCache;
        _distributedLock = distributedLock;
    }

    public virtual async Task<PlatformCertificateSecretModel> GetPlatformCertificateAsync(string mchId, string serialNo)
    {
        Check.NotNullOrWhiteSpace(mchId, nameof(mchId));
        Check.NotNullOrWhiteSpace(serialNo, nameof(serialNo));

        var cacheItem = await _distributedCache.GetAsync(PlatformCertificatesCacheItemKey);
        if (cacheItem != null)
        {
            return GetSpecifiedCachedCertificate(cacheItem, serialNo);
        }

        await using var handle = await _distributedLock.TryAcquireAsync(LockName, TimeSpan.FromSeconds(3));

        if (handle == null)
        {
            throw new InvalidOperationException("Failed to acquire the distributed lock.");
        }

        cacheItem = await _distributedCache.GetAsync(PlatformCertificatesCacheItemKey);
        if (cacheItem != null)
        {
            return GetSpecifiedCachedCertificate(cacheItem, serialNo);
        }

        var options = await _abpWeChatPayOptionsProvider.GetAsync(mchId);

        var certificateService =
            await _abpWeChatPayServiceFactory.CreateAsync<WeChatPayCertificatesWeService>(mchId);

        cacheItem = new PlatformCertificatesCacheItem();
        var cacheExpiration = TimeSpan.FromHours(6);

        try
        {
            var certificates = await certificateService.GetPlatformCertificatesAsync();

            foreach (var certificate in certificates.Data)
            {
                var certificateString = WeChatPaySecurityUtility.AesGcmDecrypt(options.ApiV3Key,
                    certificate.EncryptCertificateData.AssociatedData,
                    certificate.EncryptCertificateData.Nonce,
                    certificate.EncryptCertificateData.Ciphertext);

                cacheItem.Certificates.Add(certificate.SerialNo,
                    new PlatformCertificateSecretModel(certificate.SerialNo, certificateString,
                        certificate.EffectiveTime, certificate.ExpireTime));
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning("Fail to get and cache the platform certificates");
            _logger.LogException(e);
            cacheExpiration = TimeSpan.FromSeconds(10); // lock for 10s on failure.
        }

        await _distributedCache.SetAsync(PlatformCertificatesCacheItemKey, cacheItem,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheExpiration
            });

        return GetSpecifiedCachedCertificate(cacheItem, serialNo);
    }

    protected virtual PlatformCertificateSecretModel GetSpecifiedCachedCertificate(
        PlatformCertificatesCacheItem cacheItem, string serialNo)
    {
        if (!cacheItem.Certificates.TryGetValue(serialNo, out var secretModel))
        {
            throw new InvalidOperationException("Platform certificate not found.");
        }

        return secretModel;
    }
}