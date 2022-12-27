using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;

[Dependency(TryRegister = true)]
public class DefaultAccessTokenCache : IAccessTokenCache, ITransientDependency
{
    protected IDistributedCache<string> DistributedCache { get; }

    public DefaultAccessTokenCache(IDistributedCache<string> distributedCache)
    {
        DistributedCache = distributedCache;
    }

    public virtual Task<string> GetOrNullAsync(string key)
    {
        return DistributedCache.GetAsync(key);
    }

    public virtual Task SetAsync(string key, string value)
    {
        return DistributedCache.SetAsync(key, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
        });
    }
}