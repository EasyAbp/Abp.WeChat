using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;

public class TicketRecordingWeChatThirdPartyPlatformAuthEventHandler :
    IWeChatThirdPartyPlatformAuthEventHandler, ITransientDependency
{
    public string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.ComponentVerifyTicket;

    private readonly ILogger<TicketRecordingWeChatThirdPartyPlatformAuthEventHandler> _logger;
    private readonly IComponentVerifyTicketStore _componentVerifyTicketStore;

    public TicketRecordingWeChatThirdPartyPlatformAuthEventHandler(
        ILogger<TicketRecordingWeChatThirdPartyPlatformAuthEventHandler> logger,
        IComponentVerifyTicketStore componentVerifyTicketStore)
    {
        _logger = logger;
        _componentVerifyTicketStore = componentVerifyTicketStore;
    }

    public virtual async Task<WeChatEventHandlingResult> HandleAsync(AuthNotificationModel model)
    {
        if (model.ComponentVerifyTicket.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("缺少 ComponentVerifyTicket，通知消息无效。AppId：{0}", model.AppId);

            return new WeChatEventHandlingResult(false);
        }

        try
        {
            await _componentVerifyTicketStore.SetAsync(model.AppId, model.ComponentVerifyTicket);
        }
        catch
        {
            _logger.LogWarning(
                "ComponentVerifyTicketStore 保存出错，导致，导致 ComponentVerifyTicket 设置失败。AppId：{0}", model.AppId);

            return new WeChatEventHandlingResult(false);
        }

        return new WeChatEventHandlingResult(true);
    }
}