using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public class ReleaseTestWeChatThirdPartyPlatformAppEventHandler : IWeChatThirdPartyPlatformAppEventHandler,
    ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ReleaseTestWeChatThirdPartyPlatformAppEventHandler> _logger;
    public virtual string Event => "text";
    public int Priority => -10000;

    public ReleaseTestWeChatThirdPartyPlatformAppEventHandler(
        IServiceProvider serviceProvider,
        ILogger<ReleaseTestWeChatThirdPartyPlatformAppEventHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public virtual async Task<AppEventHandlingResult> HandleAsync(string componentAppId, string authorizerAppId,
        WeChatAppEventModel model)
    {
        if (ReleaseTestConsts.OfficialAppIds.Contains(authorizerAppId))
        {
            return await HandleOfficialReleaseTestAsync(model);
        }

        if (ReleaseTestConsts.MiniProgramsAppIds.Contains(authorizerAppId))
        {
            return await HandleMiniProgramReleaseTestAsync(model);
        }

        return new AppEventHandlingResult(true);
    }

    protected virtual async Task<AppEventHandlingResult> HandleOfficialReleaseTestAsync(WeChatAppEventModel model)
    {
        var content = model.GetProperty<string>("Content");

        if (content == "TESTCOMPONENT_MSG_TYPE_TEXT")
        {
            return new AppEventHandlingResult(true, null,
                $"<xml>"
                + $"<ToUserName><![CDATA[{model.FromUserName}]]></ToUserName>"
                + $"<FromUserName><![CDATA[{model.ToUserName}]]></FromUserName>"
                + $"<CreateTime>{(int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds}</CreateTime>"
                + $"<MsgType><![CDATA[text]]></MsgType>"
                + $"<Content><![CDATA[TESTCOMPONENT_MSG_TYPE_TEXT_callback]]></Content>"
                + $"</xml>");
        }

        if (!content.StartsWith("QUERY_AUTH_CODE:"))
        {
            return new AppEventHandlingResult(true);
        }

        var queryAuthCode = content.RemovePreFix("QUERY_AUTH_CODE:");

        try
        {
            var abpWeChatServiceFactory = _serviceProvider.GetRequiredService<IAbpWeChatServiceFactory>();
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            var jsonSerializer = _serviceProvider.GetRequiredService<IJsonSerializer>();

            var service = await abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>();

            var response = await service.QueryAuthAsync(queryAuthCode);

            var accessToken = response.AuthorizationInfo.AuthorizerAccessToken;

            var targetUrl = $"https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={accessToken}";

            var httpClient = httpClientFactory.CreateClient();

            await httpClient.PostAsync(targetUrl,
                new StringContent(jsonSerializer.Serialize(new
                {
                    touser = model.FromUserName,
                    msgtype = "text",
                    text = new
                    {
                        content = $"{queryAuthCode}_from_api"
                    }
                })));
        }
        catch (Exception e)
        {
            _logger.LogWarning("全网发布检测（公众号客服消息）处理失败。");
            _logger.LogException(e);

            return new AppEventHandlingResult(false, "全网发布检测（公众号客服消息）处理失败。");
        }

        return new AppEventHandlingResult(true);
    }

    protected virtual async Task<AppEventHandlingResult> HandleMiniProgramReleaseTestAsync(WeChatAppEventModel model)
    {
        var content = model.GetProperty<string>("Content");

        if (!content.StartsWith("QUERY_AUTH_CODE:"))
        {
            return new AppEventHandlingResult(true);
        }

        var queryAuthCode = content.RemovePreFix("QUERY_AUTH_CODE:");

        try
        {
            var abpWeChatServiceFactory = _serviceProvider.GetRequiredService<IAbpWeChatServiceFactory>();
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            var jsonSerializer = _serviceProvider.GetRequiredService<IJsonSerializer>();

            var service = await abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>();

            var response = await service.QueryAuthAsync(queryAuthCode);

            var accessToken = response.AuthorizationInfo.AuthorizerAccessToken;

            var targetUrl =
                $"https://api.weixin.qq.com/cgi-bin/message/custom/business/send?access_token={accessToken}";

            var httpClient = httpClientFactory.CreateClient();

            await httpClient.PostAsync(targetUrl,
                new StringContent(jsonSerializer.Serialize(new
                {
                    touser = model.FromUserName,
                    msgtype = "text",
                    text = new
                    {
                        content = $"{queryAuthCode}_from_api"
                    }
                })));
        }
        catch (Exception e)
        {
            _logger.LogWarning("全网发布检测（小程序客服消息）处理失败。");
            _logger.LogException(e);

            return new AppEventHandlingResult(false, "全网发布检测（小程序客服消息）处理失败。");
        }

        return new AppEventHandlingResult(true);
    }
}