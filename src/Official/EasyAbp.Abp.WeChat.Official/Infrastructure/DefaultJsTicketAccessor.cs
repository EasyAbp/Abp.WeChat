using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using EasyAbp.Abp.WeChat.Common;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public class DefaultJsTicketAccessor : IJsTicketAccessor, ISingletonDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenAccessor _accessTokenAccessor;
        private readonly IDistributedCache<string> _distributedCache;

        public DefaultJsTicketAccessor(IHttpClientFactory httpClientFactory,
            IAccessTokenAccessor accessTokenAccessor,
            IDistributedCache<string> distributedCache)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenAccessor = accessTokenAccessor;
            _distributedCache = distributedCache;
        }

        public async Task<string> GetTicketJsonAsync()
        {
            var accessToken = await _accessTokenAccessor.GetAccessTokenAsync();

            return await _distributedCache.GetOrAddAsync("CurrentJsTicket",
                async () =>
                {
                    var client = _httpClientFactory.CreateClient();
                    var requestUrl = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={accessToken}&type=jsapi";

                    return await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                        .Content.ReadAsStringAsync();
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
        }

        public async Task<string> GetTicketAsync()
        {
            var json = await GetTicketJsonAsync();
            var jObj = JObject.Parse(json);

            return jObj.SelectToken("$.ticket").Value<string>();
        }
    }
}