using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AuthorizerRefreshToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;

/// <summary>
/// 如果 AppSecret 为空，则走第三方平台的途径获得 token，否则走 DefaultAccessTokenProvider
/// </summary>
public class HybridAccessTokenProvider : IAccessTokenProvider, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthorizerAccessTokenCache _cache;
    private readonly ICurrentWeChatThirdPartyPlatform _currentWeChatThirdPartyPlatform;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> _optionsProvider;
    private readonly IAuthorizerRefreshTokenStore _authorizerRefreshTokenStore;
    private readonly IWeChatThirdPartyPlatformApiRequester _weChatThirdPartyPlatformApiRequester;

    public HybridAccessTokenProvider(
        IServiceProvider serviceProvider,
        IAuthorizerAccessTokenCache cache,
        ICurrentWeChatThirdPartyPlatform currentWeChatThirdPartyPlatform,
        IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> optionsProvider,
        IAuthorizerRefreshTokenStore authorizerRefreshTokenStore,
        IWeChatThirdPartyPlatformApiRequester weChatThirdPartyPlatformApiRequester)
    {
        _serviceProvider = serviceProvider;
        _cache = cache;
        _currentWeChatThirdPartyPlatform = currentWeChatThirdPartyPlatform;
        _optionsProvider = optionsProvider;
        _authorizerRefreshTokenStore = authorizerRefreshTokenStore;
        _weChatThirdPartyPlatformApiRequester = weChatThirdPartyPlatformApiRequester;
    }

    public virtual async Task<string> GetAsync(string appId, string appSecret)
    {
        if (appSecret.IsNullOrWhiteSpace())
        {
            var componentAppId = _currentWeChatThirdPartyPlatform.ComponentAppId;

            var options = await _optionsProvider.GetAsync(componentAppId);

            var accessToken = await _cache.GetOrNullAsync(options.AppId, appId);

            if (accessToken.IsNullOrWhiteSpace())
            {
                await _cache.SetAsync(options.AppId, appId, await RequestAuthorizerAccessTokenAsync(options, appId));
            }

            return accessToken;
        }

        var defaultAccessTokenProvider = _serviceProvider.GetRequiredService<DefaultAccessTokenProvider>();

        return await defaultAccessTokenProvider.GetAsync(appSecret, appSecret);
    }

    /// <summary>
    /// 获取/刷新接口调用令牌
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/api_authorizer_token.html
    /// </summary>
    protected virtual async Task<string> RequestAuthorizerAccessTokenAsync(
        AbpWeChatThirdPartyPlatformOptions options, string appId)
    {
        const string authorizerTokenApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token";

        var response = await _weChatThirdPartyPlatformApiRequester.RequestAsync<AuthorizerTokenResponse>(
            authorizerTokenApiUrl, HttpMethod.Post, new AuthorizerTokenRequest
            {
                ComponentAppId = options.AppId,
                AuthorizerAppId = appId,
                AuthorizerRefreshToken =
                    await _authorizerRefreshTokenStore.GetOrNullAsync(options.AppId, appId)
            }, options);

        if (response.AuthorizerAccessToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException("无法获取 authorizer_access_token，请检查授权情况或刷新令牌是否持久化");
        }

        return response.AuthorizerAccessToken;
    }
}