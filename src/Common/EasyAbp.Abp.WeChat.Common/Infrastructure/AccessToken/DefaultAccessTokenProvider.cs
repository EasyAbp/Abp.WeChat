using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken
{
    public class DefaultAccessTokenProvider : IAccessTokenProvider, ISingletonDependency
    {
        private readonly IDistributedCache<string> _distributedCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultAccessTokenProvider(IDistributedCache<string> distributedCache,
            IHttpClientFactory httpClientFactory)
        {
            _distributedCache = distributedCache;
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<string> GetAccessTokenAsync(string appId, string appSecret)
        {
            return await _distributedCache.GetOrAddAsync($"CurrentAccessToken:{appId}",
                async () =>
                {
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

                    return accessTokenObj.Value<string>();
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
        }
    }
}