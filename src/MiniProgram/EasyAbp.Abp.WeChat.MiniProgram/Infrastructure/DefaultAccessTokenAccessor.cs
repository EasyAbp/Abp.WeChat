using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public class DefaultAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
    {
        protected readonly IDistributedCache<string> DistributedCache;
        protected readonly IHttpClientFactory HttpClientFactory;
        protected readonly IWeChatMiniProgramOptionsResolver WeChatMiniProgramOptionsResolver;

        public DefaultAccessTokenAccessor(IDistributedCache<string> distributedCache,
            IHttpClientFactory httpClientFactory,
            IWeChatMiniProgramOptionsResolver weChatMiniProgramOptionsResolver)
        {
            DistributedCache = distributedCache;
            HttpClientFactory = httpClientFactory;
            WeChatMiniProgramOptionsResolver = weChatMiniProgramOptionsResolver;
        }

        public virtual async Task<string> GetAccessTokenAsync()
        {
            var options = await WeChatMiniProgramOptionsResolver.ResolveAsync();

            return await DistributedCache.GetOrAddAsync($"CurrentAccessToken:{options.AppId}",
                async () =>
                {
                    var client = HttpClientFactory.CreateClient();

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