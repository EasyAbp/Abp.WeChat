using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;

public class AuthorizerAccessTokenCache : IAuthorizerAccessTokenCache, ITransientDependency
{
    protected IAbpWeChatSharableCache AbpWeChatSharableCache { get; }

    public AuthorizerAccessTokenCache(IAbpWeChatSharableCache abpWeChatSharableCache)
    {
        AbpWeChatSharableCache = abpWeChatSharableCache;
    }

    public virtual async Task<string> GetOrNullAsync(string componentAppId, string authorizerAppId)
    {
        return await AbpWeChatSharableCache.GetOrNullAsync(await GetCacheKeyAsync(componentAppId, authorizerAppId));
    }

    public virtual async Task SetAsync(string componentAppId, string authorizerAppId, string accessToken)
    {
        await AbpWeChatSharableCache.SetAsync(await GetCacheKeyAsync(
            componentAppId, authorizerAppId), accessToken, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
        });
    }

    protected virtual async Task<string> GetCacheKeyAsync(string componentAppId, string authorizerAppId) =>
        $"AuthorizerAccessToken:{componentAppId}:{authorizerAppId}";
}