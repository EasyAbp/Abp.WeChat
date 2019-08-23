using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeChat.Common;

namespace Zony.Abp.WeiXin.Official.Infrastructure
{
    public class CacheAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
    {
        private readonly IDistributedCache<string> _distributedCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AbpWeiXinOfficialOptions _abpWeiXinOfficialOptions;

        public CacheAccessTokenAccessor(IDistributedCache<string> distributedCache,
            IHttpClientFactory httpClientFactory,
            IOptions<AbpWeiXinOfficialOptions> abpWeiXinOfficialOptions)
        {
            _distributedCache = distributedCache;
            _httpClientFactory = httpClientFactory;
            _abpWeiXinOfficialOptions = abpWeiXinOfficialOptions.Value;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await _distributedCache.GetOrAddAsync("CurrentAccessToken",
                async () =>
                {
                    var client = _httpClientFactory.CreateClient();
                    var requestUrl =
                        $"https://api.weixin.qq.com/cgi-bin/token?grant_type=${GrantTypes.ClientCredential}&appid={_abpWeiXinOfficialOptions.AppId}&secret={_abpWeiXinOfficialOptions.AppSecret}";

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