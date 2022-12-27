using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Infrastructure.AccessToken;

public class SharedStackExchangeRedisAccessTokenCache : IAccessTokenCache, ITransientDependency
{
    public static string CachePrefix { get; set; } = "WeChatTokens:";
    public static string SettingName { get; set; } = SharedCacheStackExchangeRedisSettings.RedisConfiguration;

    private readonly ISettingProvider _settingProvider;

    public SharedStackExchangeRedisAccessTokenCache(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public virtual async Task<string> GetOrNullAsync(string key)
    {
        var redisCache = await CreateAbpRedisCacheAsync();

        return await redisCache.GetStringAsync(await GetKeyAsync(key));
    }

    public virtual async Task SetAsync(string key, string value)
    {
        var redisCache = await CreateAbpRedisCacheAsync();

        await redisCache.SetStringAsync(await GetKeyAsync(key), value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
        });
    }

    protected virtual Task<string> GetKeyAsync(string key)
    {
        return Task.FromResult($"{CachePrefix}{key}");
    }

    protected virtual async Task<AbpRedisCache> CreateAbpRedisCacheAsync()
    {
        return new AbpRedisCache(new RedisCacheOptions
        {
            Configuration = await _settingProvider.GetOrNullAsync(SettingName)
        });
    }
}