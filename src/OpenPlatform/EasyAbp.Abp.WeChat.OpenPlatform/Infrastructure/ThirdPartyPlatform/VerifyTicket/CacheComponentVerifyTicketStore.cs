using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;

/// <summary>
/// 请注意，由于缓存是非持久化的，根据微信官方文档，您可能因为本缓存值的丢失而导致业务不可用。
/// 建议您重写或替换本实现，在缓存丢失时 fall back 到持久化层进行恢复。
/// 也可以使用 EasyAbp 的 WeChatManagement 模块，它已完成以上内容，实现了持久化。
/// </summary>
[Dependency(TryRegister = true)]
public class CacheComponentVerifyTicketStore : IComponentVerifyTicketStore, ISingletonDependency
{
    private readonly IDistributedCache<string> _cache;

    public CacheComponentVerifyTicketStore(IDistributedCache<string> cache)
    {
        _cache = cache;
    }

    public virtual async Task<string> GetOrNullAsync(string componentAppId)
    {
        return await _cache.GetAsync(await GetCacheKeyAsync(componentAppId));
    }

    public virtual async Task SetAsync(string componentAppId, string componentVerifyTicket)
    {
        await _cache.SetAsync(await GetCacheKeyAsync(componentAppId), componentVerifyTicket,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(715)
            });
    }

    protected virtual async Task<string> GetCacheKeyAsync(string componentAppId) =>
        $"WeChatComponentVerifyTicket:{componentAppId}";
}