using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public class AbpWeChatPayHttpClientFactory : IAbpWeChatPayHttpClientFactory, ITransientDependency
{
    public static TimeSpan SkipCertificateBytesCheckUntilDuration = TimeSpan.FromSeconds(10);

    private const string DefaultHandlerKey = "__Default__";

    protected static ConcurrentDictionary<string, Lazy<Task<HttpMessageHandlerCacheModel>>> CachedHandlers { get; } =
        new();

    protected IClock Clock { get; }
    protected IAbpLazyServiceProvider AbpLazyServiceProvider { get; }
    protected IAbpWeChatPayOptionsProvider AbpWeChatPayOptionsProvider { get; }

    public AbpWeChatPayHttpClientFactory(
        IClock clock,
        IAbpLazyServiceProvider abpLazyServiceProvider,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider)
    {
        Clock = clock;
        AbpLazyServiceProvider = abpLazyServiceProvider;
        AbpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
    }

    public virtual async Task<HttpClient> CreateAsync(string mchId)
    {
        var options = await AbpWeChatPayOptionsProvider.GetAsync(mchId);

        var handler = await GetOrCreateHttpClientHandlerAsync(options);

        return new HttpClient(handler, disposeHandler: false);
    }

    protected virtual async Task<HttpMessageHandler> GetOrCreateHttpClientHandlerAsync(AbpWeChatPayOptions options)
    {
        if (!CachedHandlers.TryGetValue(options.MchId, out var item))
        {
            return (await CachedHandlers.GetOrAdd(options.MchId ?? DefaultHandlerKey,
                _ => new Lazy<Task<HttpMessageHandlerCacheModel>>(() =>
                    CreateHttpClientHandlerCacheModelAsync(options))).Value).Handler;
        }

        var handlerCacheModel = await item.Value;

        if (handlerCacheModel.SkipCertificateBytesCheckUntil > Clock.Now)
        {
            return handlerCacheModel.Handler;
        }

        var certificateBytes = await GetCertificateBytesOrNullAsync(options);

        if (handlerCacheModel.CertificateBytes == certificateBytes &&
            handlerCacheModel.CertificateSecret == options.CertificateSecret)
        {
            return handlerCacheModel.Handler;
        }

        CachedHandlers.TryUpdate(
            options.MchId ?? DefaultHandlerKey,
            new Lazy<Task<HttpMessageHandlerCacheModel>>(() =>
                CreateHttpClientHandlerCacheModelAsync(options, certificateBytes)),
            item);

        return (await CachedHandlers.GetOrDefault(options.MchId).Value).Handler;
    }

    protected virtual async Task<byte[]> GetCertificateBytesOrNullAsync(AbpWeChatPayOptions options)
    {
        if (options.CertificateBlobName.IsNullOrEmpty())
        {
            return null;
        }

        var blobContainer = options.CertificateBlobContainerName.IsNullOrWhiteSpace()
            ? AbpLazyServiceProvider.LazyGetRequiredService<IBlobContainer>()
            : AbpLazyServiceProvider.LazyGetRequiredService<IBlobContainerFactory>()
                .Create(options.CertificateBlobContainerName);

        var certificateBytes = await blobContainer.GetAllBytesOrNullAsync(options.CertificateBlobName);
        if (certificateBytes == null)
        {
            throw new AbpException("指定的证书 Blob 无效，请重新指定有效的证书 Blob。");
        }

        return certificateBytes;
    }

    protected virtual async Task<HttpMessageHandlerCacheModel> CreateHttpClientHandlerCacheModelAsync(
        AbpWeChatPayOptions options)
    {
        var certificateBytes = await GetCertificateBytesOrNullAsync(options);

        return await CreateHttpClientHandlerCacheModelAsync(options, certificateBytes);
    }

    protected virtual Task<HttpMessageHandlerCacheModel> CreateHttpClientHandlerCacheModelAsync(
        AbpWeChatPayOptions options, byte[] certificateBytes)
    {
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
        };

        if (!certificateBytes.IsNullOrEmpty())
        {
            handler.ClientCertificates.Add(new X509Certificate2(
                certificateBytes,
                options.CertificateSecret,
                X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));

            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        }

        return Task.FromResult(new HttpMessageHandlerCacheModel
        {
            Handler = handler,
            CertificateBytes = certificateBytes,
            CertificateSecret = options.CertificateSecret,
            SkipCertificateBytesCheckUntil = Clock.Now.Add(SkipCertificateBytesCheckUntilDuration)
        });
    }
}