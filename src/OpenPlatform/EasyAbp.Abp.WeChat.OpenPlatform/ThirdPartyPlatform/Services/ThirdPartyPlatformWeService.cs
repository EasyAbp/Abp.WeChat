using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Request;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;

/// <summary>
/// 微信第三方平台的 API
/// </summary>
public class ThirdPartyPlatformWeService : ThirdPartyPlatformAbpWeChatServiceBase
{
    protected const string PreAuthCodeApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?";
    protected const string QueryAuthApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?";

    public ThirdPartyPlatformWeService(AbpWeChatThirdPartyPlatformOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
    {
    }

    /// <summary>
    /// 获取：预授权码
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/pre_auth_code.html
    /// </summary>
    public virtual async Task<PreAuthCodeResponse> GetPreAuthCodeAsync()
    {
        var url = await AppendComponentAccessTokenAsync(PreAuthCodeApiUrl);

        return await ApiRequester.RequestAsync<PreAuthCodeResponse>(
            url, HttpMethod.Post, new PreAuthCodeRequest
            {
                ComponentAppId = Options.AppId
            }, Options);
    }

    /// <summary>
    /// 使用授权码获取授权信息
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/authorization_info.html
    /// </summary>
    public virtual async Task<QueryAuthResponse> QueryAuthAsync(string authorizationCode)
    {
        var url = await AppendComponentAccessTokenAsync(QueryAuthApiUrl);

        return await ApiRequester.RequestAsync<QueryAuthResponse>(
            url, HttpMethod.Post, new QueryAuthRequest
            {
                ComponentAppId = Options.AppId,
                AuthorizationCode = authorizationCode
            }, Options);
    }

    protected virtual async Task<string> AppendComponentAccessTokenAsync(string url)
    {
        var token = await ComponentAccessTokenProvider.GetAsync(Options.AppId, Options.AppSecret);

        return url.EnsureEndsWith('?') + $"component_access_token={token}";
    }
}