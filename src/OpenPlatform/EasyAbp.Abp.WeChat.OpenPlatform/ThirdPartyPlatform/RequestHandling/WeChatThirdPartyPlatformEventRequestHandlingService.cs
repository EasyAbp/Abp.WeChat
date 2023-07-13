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

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public class WeChatThirdPartyPlatformEventRequestHandlingService :
    WeChatEventRequestHandlingServiceBase<AbpWeChatThirdPartyPlatformOptions>,
    IWeChatThirdPartyPlatformEventRequestHandlingService, ITransientDependency
{
    private readonly IWeChatThirdPartyPlatformEventHandlerResolver _weChatThirdPartyPlatformEventHandlerResolver;
    private readonly ICurrentWeChatThirdPartyPlatform _currentWeChatThirdPartyPlatform;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> _optionsProvider;

    public WeChatThirdPartyPlatformEventRequestHandlingService(
        IWeChatThirdPartyPlatformEventHandlerResolver weChatThirdPartyPlatformEventHandlerResolver,
        ICurrentWeChatThirdPartyPlatform currentWeChatThirdPartyPlatform,
        IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> optionsProvider,
        IWeChatNotificationEncryptor weChatNotificationEncryptor) : base(weChatNotificationEncryptor)
    {
        _weChatThirdPartyPlatformEventHandlerResolver = weChatThirdPartyPlatformEventHandlerResolver;
        _currentWeChatThirdPartyPlatform = currentWeChatThirdPartyPlatform;
        _optionsProvider = optionsProvider;
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

        foreach (var handler in await _weChatThirdPartyPlatformEventHandlerResolver.GetAuthEventHandlersAsync(
                     model.InfoType))
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

        IResponseToWeChatModel responseToWeChatModel = null;

        foreach (var handler in (await _weChatThirdPartyPlatformEventHandlerResolver.GetAppEventHandlersAsync(
                     model.MsgType))
                 .OrderByDescending(x => x.Priority))
        {
            var result = await handler.HandleAsync(options.AppId, input.AuthorizerAppId, model);

            if (result.ResponseToWeChatModel != null)
            {
                responseToWeChatModel = result.ResponseToWeChatModel;
            }

            if (!result.Success)
            {
                return result;
            }
        }

        return responseToWeChatModel is null
            ? new AppEventHandlingResult(true)
            : new AppEventHandlingResult(responseToWeChatModel);
    }
}