using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Infrastructure.AccessToken;

public class SharedStackExchangeRedisAbpWeChatSharableCache : IAbpWeChatSharableCache, ITransientDependency
{
    public static string CachePrefix { get; set; } = "WeChatTokens:";
    public static string SettingName { get; set; } = SharedCacheStackExchangeRedisSettings.RedisConfiguration;

    private readonly ISettingProvider _settingProvider;

    public SharedStackExchangeRedisAbpWeChatSharableCache(ISettingProvider settingProvider)
    {
        _settingProvider = settingProvider;
    }

    public virtual async Task<string> GetOrNullAsync(string key)
    {
        var redisCache = await CreateAbpRedisCacheAsync();

        return await redisCache.GetStringAsync(await GetKeyAsync(key));
    }

    public virtual async Task SetAsync(string key, string value, DistributedCacheEntryOptions options)
    {
        var redisCache = await CreateAbpRedisCacheAsync();

        if (value is null)
        {
            await redisCache.RemoveAsync(await GetKeyAsync(key));
        }
        else
        {
            await redisCache.SetStringAsync(await GetKeyAsync(key), value, options);
        }
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