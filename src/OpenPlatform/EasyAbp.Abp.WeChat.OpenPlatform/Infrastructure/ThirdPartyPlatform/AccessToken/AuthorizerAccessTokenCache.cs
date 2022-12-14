using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;

public class AuthorizerAccessTokenCache : IAuthorizerAccessTokenCache, ITransientDependency
{
    protected IDistributedCache<string> Cache { get; }

    public AuthorizerAccessTokenCache(IDistributedCache<string> cache)
    {
        Cache = cache;
    }

    public virtual async Task<string> GetAsync(string componentAppId, string authorizerAppId)
    {
        return await Cache.GetAsync(await GetCacheKeyAsync(componentAppId, authorizerAppId));
    }

    public virtual async Task SetAsync(string componentAppId, string authorizerAppId, string accessToken)
    {
        await Cache.SetAsync(
            await GetCacheKeyAsync(componentAppId, authorizerAppId),
            accessToken,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
            });
    }

    protected virtual async Task<string> GetCacheKeyAsync(string componentAppId, string authorizerAppId) =>
        $"AuthorizerAccessToken:{componentAppId}:{authorizerAppId}";
}