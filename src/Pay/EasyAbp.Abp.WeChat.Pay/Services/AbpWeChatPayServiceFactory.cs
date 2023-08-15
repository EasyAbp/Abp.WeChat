using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Pay.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services;

/// <summary>
/// 微信服务的工厂类，用于创建微信服务实例。
/// </summary>
public class AbpWeChatPayServiceFactory : IAbpWeChatPayServiceFactory, ITransientDependency
{
    protected IServiceProvider ServiceProvider { get; }

    public AbpWeChatPayServiceFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public virtual async Task<TService> CreateAsync<TService>(string mchId = null) where TService : IAbpWeChatPayService
    {
        var optionsProvider = ServiceProvider.GetRequiredService<IAbpWeChatPayOptionsProvider>();

        var options = await optionsProvider.GetAsync(mchId);

        var service = (TService)ActivatorUtilities.CreateInstance(ServiceProvider, typeof(TService), options);

        return service;
    }
}