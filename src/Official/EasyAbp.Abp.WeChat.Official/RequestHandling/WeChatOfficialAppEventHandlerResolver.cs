using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public class WeChatOfficialAppEventHandlerResolver : IWeChatOfficialAppEventHandlerResolver, ITransientDependency
{
    protected static Dictionary<string, List<Type>> AppEventHandlerCachedTypes { get; set; }

    private static readonly object SyncObj = new();

    protected IServiceProvider ServiceProvider { get; }

    public WeChatOfficialAppEventHandlerResolver(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual Task<List<IWeChatOfficialAppEventHandler>> GetAppEventHandlersAsync(string msgType)
    {
        return ResolveAppEventHandlersAsync(msgType);
    }

    protected virtual async Task<List<IWeChatOfficialAppEventHandler>> ResolveAppEventHandlersAsync(string msgType)
    {
        if (AppEventHandlerCachedTypes is null)
        {
            lock (SyncObj)
            {
                if (AppEventHandlerCachedTypes is null)
                {
                    var objs = ServiceProvider.GetServices<IWeChatOfficialAppEventHandler>().ToArray();

                    var cacheTypes = objs.GroupBy(obj => obj.MsgType)
                        .ToDictionary(x => x.Key, x => x.Select(y => y.HandlerType).ToList());

                    AppEventHandlerCachedTypes = cacheTypes;

                    return objs.Where(x => x.MsgType == msgType).ToList();
                }
            }
        }

        return AppEventHandlerCachedTypes.TryGetValue(msgType, out var type)
            ? await CreateObjectsAsync<IWeChatOfficialAppEventHandler>(type)
            : new List<IWeChatOfficialAppEventHandler>();
    }

    protected virtual Task<List<TObj>> CreateObjectsAsync<TObj>(IEnumerable<Type> types)
    {
        return Task.FromResult(types.Select(type => ServiceProvider.GetService(type)).Where(x => x != null)
            .Select(x => (TObj)x).ToList());
    }
}