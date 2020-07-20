using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Infrastructure.AccessToken
{
    [Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
    public class SharedStackExchangeRedisAccessTokenProvider : IAccessTokenProvider
    {
        public const string CachePrefix = "CurrentAccessToken:";
        public const string SettingName = SharedCacheStackExchangeRedisSettings.RedisConfiguration;

        private readonly ISettingProvider _settingProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public SharedStackExchangeRedisAccessTokenProvider(
            ISettingProvider settingProvider,
            IHttpClientFactory httpClientFactory)
        {
            _settingProvider = settingProvider;
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<string> GetAccessTokenAsync(string appId, string appSecret)
        {
            var redisCache = new AbpRedisCache(new RedisCacheOptions
            {
                Configuration = await _settingProvider.GetOrNullAsync(SettingName)
            });
            
            var key = $"{CachePrefix}{appId}";
            
            var accessToken = await redisCache.GetStringAsync(key);

            if (accessToken != null)
            {
                return accessToken;
            }
            
            var client = _httpClientFactory.CreateClient();

            var requestUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type={GrantTypes.ClientCredential}&appid={appId}&secret={appSecret}";

            var resultStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                .Content.ReadAsStringAsync();
                    
            var resultJson = JObject.Parse(resultStr);

            var accessTokenObj = resultJson.SelectToken("$.access_token");

            if (accessTokenObj == null)
            {
                throw new NullReferenceException($"无法获取到 AccessToken，微信 API 返回的内容为：{resultStr}");
            }

            accessToken = accessTokenObj.Value<string>();

            await redisCache.SetStringAsync(key, accessToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
            });

            return accessToken;
        }
    }
}