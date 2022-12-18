using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Services;

public class AbpWeChatServiceFactory : IAbpWeChatServiceFactory, ITransientDependency
{
    protected static readonly ConcurrentDictionary<Type, Type> ServiceTypeToOptionsProviderTypeMapping = new();

    protected IServiceProvider ServiceProvider { get; }

    public AbpWeChatServiceFactory(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual async Task<TService> CreateAsync<TService>(string appId = null) where TService : IAbpWeChatService
    {
        var optionsProviderType = await GetOptionsProviderTypeAsync<TService>();

        var optionsProvider = (IAbpWeChatOptionsProvider)ServiceProvider.GetRequiredService(optionsProviderType);

        var options = await optionsProvider.GetBasicAsync(appId);

        var service = (TService)ActivatorUtilities.CreateInstance(ServiceProvider, typeof(TService), options);

        return service;
    }

    protected virtual Task<Type> GetOptionsProviderTypeAsync<TService>()
    {
        return Task.FromResult(ServiceTypeToOptionsProviderTypeMapping.GetOrAdd(typeof(TService), type =>
        {
            while (type.BaseType != null)
            {
                type = type.BaseType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AbpWeChatServiceBase<,>))
                {
                    var optionsType = type.GetGenericArguments()[0];

                    return typeof(IAbpWeChatOptionsProvider<>).MakeGenericType(optionsType);
                }
            }

            throw new AbpException($"Invalid WeChat service type: {typeof(TService).Name}");
        }));
    }
}