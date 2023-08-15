using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public class AbpWeChatPayHttpClientFactory : IAbpWeChatPayHttpClientFactory, ITransientDependency
{
    // TODO: Confirm checking every 10 second? @gdlc88
    public static TimeSpan SkipCertificateBytesCheckUntilDuration = TimeSpan.FromSeconds(10);

    private const string DefaultHandlerKey = "__Default__";

    protected static ConcurrentDictionary<string, Lazy<Task<HttpMessageHandlerCacheModel>>> CachedHandlers { get; } =
        new();

    protected IClock Clock { get; }
    protected IAbpLazyServiceProvider AbpLazyServiceProvider { get; }
    protected IAbpWeChatPayOptionsProvider AbpWeChatPayOptionsProvider { get; }
    protected ICertificatesManager CertificatesManager { get; }

    public AbpWeChatPayHttpClientFactory(
        IClock clock,
        IAbpLazyServiceProvider abpLazyServiceProvider,
        IAbpWeChatPayOptionsProvider abpWeChatPayOptionsProvider,
        ICertificatesManager certificatesManager)
    {
        Clock = clock;
        AbpLazyServiceProvider = abpLazyServiceProvider;
        AbpWeChatPayOptionsProvider = abpWeChatPayOptionsProvider;
        CertificatesManager = certificatesManager;
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

        var certificate = await CertificatesManager.GetCertificateAsync(options.MchId);
        if (handlerCacheModel.WeChatPayCertificate.CertificateHashCode.VerifySha256(certificate.CertificateHashCode))
        {
            return handlerCacheModel.Handler;
        }

        // If the certificate has expired, need to pull the latest one from BLOB again.
        CachedHandlers.TryUpdate(
            options.MchId ?? DefaultHandlerKey,
            new Lazy<Task<HttpMessageHandlerCacheModel>>(() =>
                CreateHttpClientHandlerCacheModelAsync(certificate)),
            item);

        return (await CachedHandlers.GetOrDefault(options.MchId).Value).Handler;
    }

    protected virtual async Task<HttpMessageHandlerCacheModel> CreateHttpClientHandlerCacheModelAsync(AbpWeChatPayOptions options)
    {
        var certificate = await CertificatesManager.GetCertificateAsync(options.MchId);
        return await CreateHttpClientHandlerCacheModelAsync(certificate);
    }

    protected virtual Task<HttpMessageHandlerCacheModel> CreateHttpClientHandlerCacheModelAsync(WeChatPayCertificate weChatPayCertificate)
    {
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
        };

        if (weChatPayCertificate.X509Certificate != null)
        {
            handler.ClientCertificates.Add(weChatPayCertificate.X509Certificate);
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        }

        return Task.FromResult(new HttpMessageHandlerCacheModel
        {
            Handler = handler,
            WeChatPayCertificate = weChatPayCertificate,
            SkipCertificateBytesCheckUntil = Clock.Now.Add(SkipCertificateBytesCheckUntilDuration)
        });
    }
}