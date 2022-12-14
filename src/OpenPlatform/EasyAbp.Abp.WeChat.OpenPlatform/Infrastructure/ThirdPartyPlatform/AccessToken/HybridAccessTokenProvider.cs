using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AuthorizerRefreshToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.ComponentAccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;

/// <summary>
/// 如果 AppSecret 为空，则走第三方平台的途径获得 token，否则走 DefaultAccessTokenProvider
/// </summary>
public class HybridAccessTokenProvider : IAccessTokenProvider, ISingletonDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthorizerAccessTokenCache _cache;
    private readonly IAuthorizerRefreshTokenStore _authorizerRefreshTokenStore;
    private readonly IWeChatThirdPartyPlatformOptionsResolver _weChatThirdPartyPlatformOptionsResolver;
    private readonly IComponentAccessTokenProvider _componentAccessTokenProvider;
    private readonly IWeChatOpenPlatformApiRequester _weChatOpenPlatformApiRequester;

    public HybridAccessTokenProvider(
        IServiceProvider serviceProvider,
        IAuthorizerAccessTokenCache cache,
        IAuthorizerRefreshTokenStore authorizerRefreshTokenStore,
        IWeChatThirdPartyPlatformOptionsResolver weChatThirdPartyPlatformOptionsResolver,
        IComponentAccessTokenProvider componentAccessTokenProvider,
        IWeChatOpenPlatformApiRequester weChatOpenPlatformApiRequester)
    {
        _serviceProvider = serviceProvider;
        _cache = cache;
        _authorizerRefreshTokenStore = authorizerRefreshTokenStore;
        _weChatThirdPartyPlatformOptionsResolver = weChatThirdPartyPlatformOptionsResolver;
        _componentAccessTokenProvider = componentAccessTokenProvider;
        _weChatOpenPlatformApiRequester = weChatOpenPlatformApiRequester;
    }

    public virtual async Task<string> GetAccessTokenAsync(string appId, string appSecret)
    {
        if (appSecret.IsNullOrWhiteSpace())
        {
            var options = await _weChatThirdPartyPlatformOptionsResolver.ResolveAsync();

            var accessToken = await _cache.GetAsync(options.AppId, appId);

            if (accessToken.IsNullOrWhiteSpace())
            {
                await _cache.SetAsync(options.AppId, appId, await RequestAuthorizerAccessTokenAsync(options, appId));
            }

            return accessToken;
        }

        var defaultAccessTokenProvider = _serviceProvider.GetRequiredService<DefaultAccessTokenProvider>();

        return await defaultAccessTokenProvider.GetAccessTokenAsync(appSecret, appSecret);
    }

    /// <summary>
    /// 获取/刷新接口调用令牌
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/api_authorizer_token.html
    /// </summary>
    protected virtual async Task<string> RequestAuthorizerAccessTokenAsync(
        IWeChatThirdPartyPlatformOptions options, string appId)
    {
        const string authorizerTokenApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token";

        var url = await AppendComponentAccessTokenAsync(authorizerTokenApiUrl, options);

        var response = await _weChatOpenPlatformApiRequester.RequestAsync<AuthorizerTokenResponse>(
            url, HttpMethod.Post, new AuthorizerTokenRequest
            {
                ComponentAppId = options.AppId,
                AuthorizerAppId = appId,
                AuthorizerRefreshToken =
                    await _authorizerRefreshTokenStore.GetOrNullAsync(options.AppId, appId)
            });

        if (response.AuthorizerAccessToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException("无法获取 authorizer_access_token，请检查授权情况或刷新令牌是否持久化");
        }

        return response.AuthorizerAccessToken;
    }

    protected virtual async Task<string> AppendComponentAccessTokenAsync(
        string url, IWeChatThirdPartyPlatformOptions options)
    {
        var token = await _componentAccessTokenProvider.GetAsync(options.AppId, options.AppSecret);

        return url.EnsureEndsWith('?') + $"component_access_token={token}";
    }
}