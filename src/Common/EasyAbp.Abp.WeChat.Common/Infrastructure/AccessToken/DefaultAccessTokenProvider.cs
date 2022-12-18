using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken
{
    public class DefaultAccessTokenProvider : IAccessTokenProvider, ITransientDependency
    {
        private readonly IAccessTokenCache _accessTokenCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultAccessTokenProvider(
            IAccessTokenCache accessTokenCache,
            IHttpClientFactory httpClientFactory)
        {
            _accessTokenCache = accessTokenCache;
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<string> GetAsync(string appId, string appSecret)
        {
            var cacheKey = $"CurrentAccessToken:{appId}";

            var token = await _accessTokenCache.GetOrNullAsync(cacheKey);

            if (token.IsNullOrWhiteSpace())
            {
                token = await RequestAccessTokenAsync(appId, appSecret);

                await _accessTokenCache.SetAsync(cacheKey, token);
            }

            return token;
        }

        protected virtual async Task<string> RequestAccessTokenAsync(string appId, string appSecret)
        {
            var client = _httpClientFactory.CreateClient();

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