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
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public class WeChatThirdPartyPlatformEventRequestHandlingService :
    IWeChatThirdPartyPlatformEventRequestHandlingService, ITransientDependency
{
    private readonly IThirdPartyPlatformEventHandlerResolver _thirdPartyPlatformEventHandlerResolver;
    private readonly ICurrentWeChatThirdPartyPlatform _currentWeChatThirdPartyPlatform;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> _optionsProvider;
    private readonly IWeChatNotificationEncryptor _weChatNotificationEncryptor;

    public WeChatThirdPartyPlatformEventRequestHandlingService(
        IThirdPartyPlatformEventHandlerResolver thirdPartyPlatformEventHandlerResolver,
        ICurrentWeChatThirdPartyPlatform currentWeChatThirdPartyPlatform,
        IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> optionsProvider,
        IWeChatNotificationEncryptor weChatNotificationEncryptor)
    {
        _thirdPartyPlatformEventHandlerResolver = thirdPartyPlatformEventHandlerResolver;
        _currentWeChatThirdPartyPlatform = currentWeChatThirdPartyPlatform;
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

        foreach (var handler in await _thirdPartyPlatformEventHandlerResolver.GetAuthEventHandlersAsync(model.InfoType))
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
    public virtual async Task<AppEventHandlingResult> NotifyAppAsync(NotifyAppInput input)
    {
        var options = await _optionsProvider.GetAsync(input.ComponentAppId);

        using var currentPlatform = _currentWeChatThirdPartyPlatform.Change(input.ComponentAppId);

        var model = await DecryptMsgAsync<WeChatAppEventModel>(options, input.EventRequest);

        string specifiedResponseContent = null;

        foreach (var handler in (await _thirdPartyPlatformEventHandlerResolver.GetAppEventHandlersAsync(model.MsgType))
                 .OrderByDescending(x => x.Priority))
        {
            var result = await handler.HandleAsync(options.AppId, input.AuthorizerAppId, model);

            if (result.SpecifiedResponseContent != null)
            {
                specifiedResponseContent = result.SpecifiedResponseContent;
            }

            if (!result.Success)
            {
                return result;
            }
        }

        return new AppEventHandlingResult(true, null, specifiedResponseContent);
    }

    protected virtual async Task<T> DecryptMsgAsync<T>(AbpWeChatThirdPartyPlatformOptions options,
        WeChatEventRequestModel request) where T : ExtensibleObject, new()
    {
        return await _weChatNotificationEncryptor.DecryptAsync<T>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            request.MsgSignature,
            request.Timestamp,
            request.Notice,
            request.PostData);
    }
}