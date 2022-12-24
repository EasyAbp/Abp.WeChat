using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public class ThirdPartyPlatformEventHandlerResolver : IThirdPartyPlatformEventHandlerResolver, ITransientDependency
{
    protected static Dictionary<string, List<Type>> AuthEventHandlerCachedTypes { get; set; }
    protected static Dictionary<string, List<Type>> AppEventHandlerCachedTypes { get; set; }

    private static readonly object SyncObj1 = new();
    private static readonly object SyncObj2 = new();

    protected IServiceProvider ServiceProvider { get; }

    public ThirdPartyPlatformEventHandlerResolver(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual Task<List<IWeChatThirdPartyPlatformAuthEventHandler>> GetAuthEventHandlersAsync(
        string infoType)
    {
        return ResolveAuthEventHandlersAsync(infoType);
    }

    public virtual Task<List<IWeChatThirdPartyPlatformAppEventHandler>> GetAppEventHandlersAsync(string msgType)
    {
        return ResolveAppEventHandlersAsync(msgType);
    }

    protected virtual async Task<List<IWeChatThirdPartyPlatformAuthEventHandler>> ResolveAuthEventHandlersAsync(
        string infoType)
    {
        if (AuthEventHandlerCachedTypes is null)
        {
            lock (SyncObj1)
            {
                if (AuthEventHandlerCachedTypes is null)
                {
                    var objs = ServiceProvider.GetServices<IWeChatThirdPartyPlatformAuthEventHandler>().ToArray();

                    var cacheTypes = objs.GroupBy(obj => obj.InfoType)
                        .ToDictionary(x => x.Key, x => x.Select(y => y.GetType()).ToList());

                    AuthEventHandlerCachedTypes = cacheTypes;

                    return objs.Where(x => x.InfoType == infoType).ToList();
                }
            }
        }

        return AuthEventHandlerCachedTypes.ContainsKey(infoType)
            ? await CreateObjectsAsync<IWeChatThirdPartyPlatformAuthEventHandler>(AuthEventHandlerCachedTypes[infoType])
            : new List<IWeChatThirdPartyPlatformAuthEventHandler>();
    }

    protected virtual async Task<List<IWeChatThirdPartyPlatformAppEventHandler>> ResolveAppEventHandlersAsync(
        string msgType)
    {
        if (AppEventHandlerCachedTypes is null)
        {
            lock (SyncObj2)
            {
                if (AppEventHandlerCachedTypes is null)
                {
                    var objs = ServiceProvider.GetServices<IWeChatThirdPartyPlatformAppEventHandler>().ToArray();

                    var cacheTypes = objs.GroupBy(obj => obj.MsgType)
                        .ToDictionary(x => x.Key, x => x.Select(y => y.GetType()).ToList());

                    AppEventHandlerCachedTypes = cacheTypes;

                    return objs.Where(x => x.MsgType == msgType).ToList();
                }
            }
        }

        return AppEventHandlerCachedTypes.ContainsKey(msgType)
            ? await CreateObjectsAsync<IWeChatThirdPartyPlatformAppEventHandler>(AppEventHandlerCachedTypes[msgType])
            : new List<IWeChatThirdPartyPlatformAppEventHandler>();
    }

    protected virtual Task<List<TObj>> CreateObjectsAsync<TObj>(IEnumerable<Type> types)
    {
        return Task.FromResult(types.Select(type => ServiceProvider.GetService(type)).Where(x => x != null)
            .Select(x => (TObj)x).ToList());
    }
}