using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;

public class AuthorizerAccessTokenCache : IAuthorizerAccessTokenCache, ITransientDependency
{
    protected IAccessTokenCache AccessTokenCache { get; }

    public AuthorizerAccessTokenCache(IAccessTokenCache accessTokenCache)
    {
        AccessTokenCache = accessTokenCache;
    }

    public virtual async Task<string> GetAsync(string componentAppId, string authorizerAppId)
    {
        return await AccessTokenCache.GetOrNullAsync(await GetCacheKeyAsync(componentAppId, authorizerAppId));
    }

    public virtual async Task SetAsync(string componentAppId, string authorizerAppId, string accessToken)
    {
        await AccessTokenCache.SetAsync(await GetCacheKeyAsync(componentAppId, authorizerAppId), accessToken);
    }

    protected virtual async Task<string> GetCacheKeyAsync(string componentAppId, string authorizerAppId) =>
        $"AuthorizerAccessToken:{componentAppId}:{authorizerAppId}";
}