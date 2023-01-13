using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.VerifyTicket;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ComponentAccessToken;

public class DefaultComponentAccessTokenProvider : IComponentAccessTokenProvider, ITransientDependency
{
    private readonly IAbpWeChatSharableCache _cache;
    private readonly IComponentVerifyTicketStore _componentVerifyTicketStore;
    private readonly IWeChatThirdPartyPlatformApiRequester _apiRequester;

    public DefaultComponentAccessTokenProvider(
        IAbpWeChatSharableCache cache,
        IComponentVerifyTicketStore componentVerifyTicketStore,
        IWeChatThirdPartyPlatformApiRequester apiRequester)
    {
        _cache = cache;
        _componentVerifyTicketStore = componentVerifyTicketStore;
        _apiRequester = apiRequester;
    }

    public virtual async Task<string> GetAsync(string componentAppId, string componentAppSecret)
    {
        Check.NotNullOrWhiteSpace(componentAppId, nameof(componentAppId));
        Check.NotNullOrWhiteSpace(componentAppSecret, nameof(componentAppSecret));

        var key = $"ComponentAccessToken:{componentAppId}";

        var cachedValue = await _cache.GetOrNullAsync(key);

        if (cachedValue.IsNullOrEmpty())
        {
            cachedValue = await RequestComponentAccessTokenAsync(componentAppId, componentAppSecret);

            await _cache.SetAsync(
                key,
                cachedValue,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(115)
                });
        }

        return cachedValue;
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
            }, null);

        if (response.ComponentAccessToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException("无法获取 component_access_token，请检查第三方平台配置");
        }

        return response.ComponentAccessToken;
    }
}