using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Infrastructure.AccessToken
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    public class SharedStackExchangeRedisAccessTokenProvider : IAccessTokenCache
    {
        public const string CachePrefix = "CurrentAccessToken:";
        public const string SettingName = SharedCacheStackExchangeRedisSettings.RedisConfiguration;

        private readonly ISettingProvider _settingProvider;

        public SharedStackExchangeRedisAccessTokenProvider(ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        public virtual async Task<string> GetOrNullAsync(string key)
        {
            var redisCache = await CreateAbpRedisCacheAsync();

            return await redisCache.GetStringAsync($"{CachePrefix}{key}");
        }

        public virtual async Task SetAsync(string key, string value)
        {
            var redisCache = await CreateAbpRedisCacheAsync();

            await redisCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
            });
        }

        protected virtual async Task<AbpRedisCache> CreateAbpRedisCacheAsync()
        {
            return new AbpRedisCache(new RedisCacheOptions
            {
                Configuration = await _settingProvider.GetOrNullAsync(SettingName)
            });
        }
    }
}