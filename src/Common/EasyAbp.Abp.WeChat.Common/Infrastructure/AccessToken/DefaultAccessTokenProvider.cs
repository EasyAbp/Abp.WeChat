using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken
{
    public class DefaultAccessTokenProvider : IAccessTokenProvider, ITransientDependency
    {
        private readonly IAbpWeChatSharableCache _abpWeChatSharableCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultAccessTokenProvider(
            IAbpWeChatSharableCache abpWeChatSharableCache,
            IHttpClientFactory httpClientFactory)
        {
            _abpWeChatSharableCache = abpWeChatSharableCache;
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<string> GetAsync(string appId, string appSecret)
        {
            var cacheKey = await GetCacheKeyAsync(appId);

            var token = await _abpWeChatSharableCache.GetOrNullAsync(cacheKey);

            if (token.IsNullOrWhiteSpace())
            {
                token = await RequestAccessTokenAsync(appId, appSecret);

                await _abpWeChatSharableCache.SetAsync(cacheKey, token, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
            }

            return token;
        }

        protected virtual Task<string> GetCacheKeyAsync(string appId) => Task.FromResult($"CurrentAccessToken:{appId}");

        protected virtual async Task<string> RequestAccessTokenAsync(string appId, string appSecret)
        {
            var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);

            var requestUrl = $"https://api.weixin.qq.com/cgi-bin/token" +
                             $"?grant_type={GrantTypes.ClientCredential}" +
                             $"&appid={appId}" +
                             $"&secret={appSecret}";

            var resultStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                .Content.ReadAsStringAsync();

            var resultJson = JObject.Parse(resultStr);

            var accessTokenObj = resultJson.SelectToken("$.access_token");

            if (accessTokenObj == null)
            {
                throw new NullReferenceException($"无法获取到 AccessToken，微信 API 返回的内容为：{resultStr}");
            }

            return accessTokenObj.Value<string>();
        }
    }
}