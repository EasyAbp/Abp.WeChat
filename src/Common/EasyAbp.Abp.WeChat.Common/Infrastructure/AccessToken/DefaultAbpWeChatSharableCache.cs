using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;

[Dependency(TryRegister = true)]
public class DefaultAbpWeChatSharableCache : IAbpWeChatSharableCache, ITransientDependency
{
    protected IDistributedCache<string> DistributedCache { get; }

    public DefaultAbpWeChatSharableCache(IDistributedCache<string> distributedCache)
    {
        DistributedCache = distributedCache;
    }

    public virtual Task<string> GetOrNullAsync(string key)
    {
        return DistributedCache.GetAsync(key);
    }

    public virtual Task SetAsync(string key, string value, DistributedCacheEntryOptions options)
    {
        return DistributedCache.SetAsync(key, value, options);
    }
}