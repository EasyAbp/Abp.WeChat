using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Request;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform;

/// <summary>
/// 微信第三方平台的 API
/// </summary>
public class ThirdPartyPlatformApiService : ThirdPartyPlatformCommonService
{
    protected const string PreAuthCodeApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?";
    protected const string QueryAuthApiUrl = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?";

    private readonly IWeChatThirdPartyPlatformOptionsResolver _optionsResolver;

    public ThirdPartyPlatformApiService(IWeChatThirdPartyPlatformOptionsResolver optionsResolver)
    {
        _optionsResolver = optionsResolver;
    }

    /// <summary>
    /// 获取：预授权码
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/pre_auth_code.html
    /// </summary>
    public virtual async Task<PreAuthCodeResponse> GetPreAuthCodeAsync()
    {
        var options = await _optionsResolver.ResolveAsync();
        var url = await AppendComponentAccessTokenAsync(PreAuthCodeApiUrl, options);

        return await WeChatOpenPlatformApiRequester.RequestAsync<PreAuthCodeResponse>(
            url, HttpMethod.Post, new PreAuthCodeRequest
            {
                ComponentAppId = options.AppId
            });
    }

    /// <summary>
    /// 使用授权码获取授权信息
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/authorization_info.html
    /// </summary>
    public virtual async Task<QueryAuthResponse> QueryAuthAsync(string authorizationCode)
    {
        var options = await _optionsResolver.ResolveAsync();
        var url = await AppendComponentAccessTokenAsync(QueryAuthApiUrl, options);

        return await WeChatOpenPlatformApiRequester.RequestAsync<QueryAuthResponse>(
            url, HttpMethod.Post, new QueryAuthRequest
            {
                ComponentAppId = options.AppId,
                AuthorizationCode = authorizationCode
            });
    }

    protected virtual async Task<string> AppendComponentAccessTokenAsync(
        string url, IWeChatThirdPartyPlatformOptions options)
    {
        var token = await ComponentAccessTokenProvider.GetAsync(options.AppId, options.AppSecret);

        return url.EnsureEndsWith('?') + $"component_access_token={token}";
    }
}