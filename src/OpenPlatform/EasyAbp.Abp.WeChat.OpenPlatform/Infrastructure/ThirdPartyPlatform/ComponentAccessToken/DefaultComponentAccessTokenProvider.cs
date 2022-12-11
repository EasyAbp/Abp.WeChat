using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.ComponentAccessToken;

public class DefaultComponentAccessTokenProvider : IComponentAccessTokenProvider, ISingletonDependency
{
    private readonly IDistributedCache<string> _distributedCache;
    private readonly IComponentVerifyTicketStore _componentVerifyTicketStore;
    private readonly IWeChatOpenPlatformApiRequester _apiRequester;

    public DefaultComponentAccessTokenProvider(
        IDistributedCache<string> distributedCache,
        IComponentVerifyTicketStore componentVerifyTicketStore,
        IWeChatOpenPlatformApiRequester apiRequester)
    {
        _distributedCache = distributedCache;
        _componentVerifyTicketStore = componentVerifyTicketStore;
        _apiRequester = apiRequester;
    }

    public virtual async Task<string> GetAsync(string componentAppId, string componentAppSecret)
    {
        Check.NotNullOrWhiteSpace(componentAppId, nameof(componentAppId));
        Check.NotNullOrWhiteSpace(componentAppSecret, nameof(componentAppSecret));

        return await _distributedCache.GetOrAddAsync($"ComponentAccessToken:{componentAppId}",
            async () => await RequestComponentAccessTokenAsync(componentAppId, componentAppSecret),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
            });
    }

    protected virtual async Task<string> RequestComponentAccessTokenAsync(
        string componentAppId, string componentAppSecret)
    {
        Check.NotNullOrWhiteSpace(componentAppId, nameof(componentAppId));
        Check.NotNullOrWhiteSpace(componentAppSecret, nameof(componentAppSecret));

        var componentVerifyTicket = await _componentVerifyTicketStore.GetOrNullAsync(componentAppId);

        if (componentVerifyTicket.IsNullOrWhiteSpace())
        {
            throw new BusinessException(
                "无法获取 component_verify_ticket，可能是微信第三方平台配置有误，或是 component_verify_ticket 没有持久化，或是微信服务器还没来得及推送");
        }

        const string requestUrl = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";

        var response = await _apiRequester.RequestAsync<ApiComponentTokenResponse>(
            requestUrl, HttpMethod.Post, new ApiComponentTokenRequest
            {
                ComponentAppId = componentAppId,
                ComponentAppSecret = componentAppSecret,
                ComponentVerifyTicket = componentVerifyTicket
            });

        if (response.ComponentAccessToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException("无法获取 component_access_token，请检查第三方平台配置");
        }

        return response.ComponentAccessToken;
    }
}