using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Exceptions;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DistributedLocking;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public class PlatformCertificateManager : IPlatformCertificateManager
{
    private readonly IAbpWeChatPayServiceFactory _abpWeChatPayServiceFactory;
    private readonly IAbpWeChatPayOptionsProvider _abpWeChatPayOptionsProvider;
    private readonly IDistributedCache<PlatformCertificateCacheItem> _distributedCache;
    private readonly IAbpDistributedLock _distributedLock;

    public PlatformCertificateManager(IAbpWeChatPayServiceFactory abpWeChatPayServiceFactory,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider,
        IDistributedCache<PlatformCertificateCacheItem> distributedCache,
        IAbpDistributedLock distributedLock)
    {
        _abpWeChatPayServiceFactory = abpWeChatPayServiceFactory;
        _abpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
        _distributedCache = distributedCache;
        _distributedLock = distributedLock;
    }

    public virtual async Task<PlatformCertificateCacheItem> GetPlatformCertificateAsync(string serialNo)
    {
        var cacheItem = await _distributedCache.GetAsync(serialNo);
        if (cacheItem == null)
        {
            await using var handle = await _distributedLock.TryAcquireAsync(serialNo);

            if (handle != null)
            {
                cacheItem = await _distributedCache.GetAsync(serialNo);
                if (cacheItem == null)
                {
                    var certificateService = await _abpWeChatPayServiceFactory.CreateAsync<WeChatPayCertificatesWeService>();
                    var certificates = await certificateService.GetWeChatPayCertificatesAsync();
                    var matchCertificate = certificates.Data.FirstOrDefault(x => x.SerialNo == serialNo);
                    if (matchCertificate == null)
                    {
                        throw new CallWeChatPayApiException("不合法的平台证书序号。");
                    }

                    var options = await _abpWeChatPayOptionsProvider.GetAsync(certificateService.MchId);

                    var certificateString = WeChatPaySecurityUtility.AesGcmDecrypt(options.ApiV3Key,
                        matchCertificate.EncryptCertificateData.AssociatedData,
                        matchCertificate.EncryptCertificateData.Nonce,
                        matchCertificate.EncryptCertificateData.Ciphertext);

                    cacheItem = new PlatformCertificateCacheItem
                    {
                        SerialNo = matchCertificate.SerialNo,
                        Certificate = certificateString
                    };

                    await _distributedCache.SetAsync(matchCertificate.SerialNo, cacheItem, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = matchCertificate.ExpireTime
                    });
                }
                else
                {
                    throw new InvalidOperationException("Failed to acquire the distributed lock.");
                }
            }
        }

        return cacheItem;
    }
}