using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official.Options;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.JsTickets
{
    public class DefaultJsTicketProvider : IJsTicketProvider, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> _optionsProvider;
        private readonly IDistributedCache<string> _distributedCache;

        public DefaultJsTicketProvider(
            IHttpClientFactory httpClientFactory,
            IAccessTokenProvider accessTokenProvider,
            IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> optionsProvider,
            IDistributedCache<string> distributedCache)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenProvider = accessTokenProvider;
            _optionsProvider = optionsProvider;
            _distributedCache = distributedCache;
        }

        public virtual async Task<string> GetTicketJsonAsync(string appId, string appSecret)
        {
            var options = await _optionsProvider.GetAsync(appId);

            var accessToken = await _accessTokenProvider.GetAsync(appId, appSecret);

            return await _distributedCache.GetOrAddAsync(await GetJsTicketAsync(options),
                async () =>
                {
                    var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);
                    var requestUrl =
                        $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={accessToken}&type=jsapi";

                    return await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                        .Content.ReadAsStringAsync();
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
        }

        protected virtual async Task<string> GetJsTicketAsync(IAbpWeChatOptions options)
        {
            return $"WeChatJsTicket:{options.AppId}";
        }

        public virtual async Task<string> GetTicketAsync(string appId, string appSecret)
        {
            var json = await GetTicketJsonAsync(appId, appSecret);
            var jObj = JObject.Parse(json);

            return jObj.SelectToken("$.ticket")!.Value<string>();
        }
    }
}