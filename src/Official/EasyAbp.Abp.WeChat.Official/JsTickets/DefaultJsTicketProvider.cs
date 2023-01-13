using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official.Options;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.JsTickets
{
    public class DefaultJsTicketProvider : IJsTicketProvider, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> _optionsProvider;
        private readonly IAbpWeChatSharableCache _cache;

        public DefaultJsTicketProvider(
            IHttpClientFactory httpClientFactory,
            IAccessTokenProvider accessTokenProvider,
            IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> optionsProvider,
            IAbpWeChatSharableCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenProvider = accessTokenProvider;
            _optionsProvider = optionsProvider;
            _cache = cache;
        }

        public virtual async Task<string> GetTicketJsonAsync(string appId, string appSecret)
        {
            var cacheKey = await GetCacheKeyAsync(appId);

            var cachedValue = await _cache.GetOrNullAsync(cacheKey);

            if (cachedValue.IsNullOrEmpty())
            {
                cachedValue = await RequestTicketAsync(appId, appSecret);

                await _cache.SetAsync(cacheKey, cachedValue, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
            }

            return cacheKey;
        }

        protected virtual Task<string> GetCacheKeyAsync(string appId)
        {
            return Task.FromResult($"WeChatJsTicket:{appId}");
        }

        public virtual async Task<string> GetTicketAsync(string appId, string appSecret)
        {
            var json = await GetTicketJsonAsync(appId, appSecret);
            var jObj = JObject.Parse(json);

            return jObj.SelectToken("$.ticket")!.Value<string>();
        }

        protected virtual async Task<string> RequestTicketAsync(string appId, string appSecret)
        {
            var accessToken = await _accessTokenProvider.GetAsync(appId, appSecret);

            var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);
            var requestUrl =
                $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={accessToken}&type=jsapi";

            return await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                .Content.ReadAsStringAsync();
        }
    }
}