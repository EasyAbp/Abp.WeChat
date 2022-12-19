using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public class WeChatThirdPartyPlatformEventRequestHandlingService :
    IWeChatThirdPartyPlatformEventRequestHandlingService, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> _optionsProvider;
    private readonly IWeChatNotificationEncryptor _weChatNotificationEncryptor;

    public WeChatThirdPartyPlatformEventRequestHandlingService(
        IServiceProvider serviceProvider,
        IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> optionsProvider,
        IWeChatNotificationEncryptor weChatNotificationEncryptor)
    {
        _serviceProvider = serviceProvider;
        _optionsProvider = optionsProvider;
        _weChatNotificationEncryptor = weChatNotificationEncryptor;
    }

    /// <summary>
    /// 授权事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAuthEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/component_verify_ticket.html
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    public virtual async Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input)
    {
        var options = await _optionsProvider.GetAsync(input.ComponentAppId);

        var model = await DecryptMsgAsync<AuthEventModel>(options, input.EventRequest);

        var handlers = _serviceProvider.GetService<IEnumerable<IWeChatThirdPartyPlatformAuthEventHandler>>();

        foreach (var handler in handlers.Where(x => x.InfoType == model.InfoType))
        {
            var result = await handler.HandleAsync(model);

            if (!result.Success)
            {
                return result;
            }
        }

        return new WeChatRequestHandlingResult(true);
    }

    /// <summary>
    /// 微信应用事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAppEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/operation/thirdparty/prepare.html
    /// </summary>
    public virtual async Task<WeChatRequestHandlingResult> NotifyAppAsync(NotifyAppInput input)
    {
        var options = await _optionsProvider.GetAsync(input.ComponentAppId);

        var model = await DecryptMsgAsync<WeChatAppEventModel>(options, input.EventRequest);

        var handlers = _serviceProvider.GetService<IEnumerable<IWeChatThirdPartyPlatformAppEventHandler>>();

        foreach (var handler in handlers.Where(x => x.Event == model.Event))
        {
            var result = await handler.HandleAsync(options.AppId, input.AuthorizerAppId, model);

            if (!result.Success)
            {
                return result;
            }
        }

        return new WeChatRequestHandlingResult(true);
    }

    protected virtual async Task<T> DecryptMsgAsync<T>(AbpWeChatThirdPartyPlatformOptions options,
        WeChatEventRequestModel request) where T : ExtensibleObject, new()
    {
        return await _weChatNotificationEncryptor.DecryptPostDataAsync<T>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            request.MsgSignature,
            request.Timestamp,
            request.Notice,
            request.PostData);
    }
}