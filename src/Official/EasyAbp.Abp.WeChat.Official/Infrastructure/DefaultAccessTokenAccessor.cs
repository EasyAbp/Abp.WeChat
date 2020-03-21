using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public class DefaultAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
    {
        protected readonly IDistributedCache<string> DistributedCache;
        protected readonly IHttpClientFactory HttpClientFactory;
        protected readonly IWeChatOfficialOptionsResolver WeChatOfficialOptionsResolver;

        public DefaultAccessTokenAccessor(IDistributedCache<string> distributedCache,
            IHttpClientFactory httpClientFactory,
            IWeChatOfficialOptionsResolver weChatOfficialOptionsResolver)
        {
            DistributedCache = distributedCache;
            HttpClientFactory = httpClientFactory;
            WeChatOfficialOptionsResolver = weChatOfficialOptionsResolver;
        }

        public virtual async Task<string> GetAccessTokenAsync()
        {
            return await DistributedCache.GetOrAddAsync("CurrentAccessToken",
                async () =>
                {
                    var client = HttpClientFactory.CreateClient();
                    var options = WeChatOfficialOptionsResolver.Resolve();

                    var requestUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type={GrantTypes.ClientCredential}&appid={options.AppId}&secret={options.AppSecret}";

                    var resultStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUrl)))
                        .Content.ReadAsStringAsync();
                    var resultJson = JObject.Parse(resultStr);

                    var accessTokenObj = resultJson.SelectToken("$.access_token");
                    if (accessTokenObj == null)
                        throw new NullReferenceException($"无法获取到 AccessToken，微信 API 返回的内容为：{resultStr}");

                    return resultJson.SelectToken("$.access_token").Value<string>();
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
        }
    }
}