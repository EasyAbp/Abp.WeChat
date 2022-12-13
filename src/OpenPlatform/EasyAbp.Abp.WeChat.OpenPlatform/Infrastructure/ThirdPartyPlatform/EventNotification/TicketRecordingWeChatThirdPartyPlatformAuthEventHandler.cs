using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;

public class TicketRecordingWeChatThirdPartyPlatformAuthEventHandler : IWeChatThirdPartyPlatformAuthEventHandler
{
    public string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.ComponentVerifyTicket;

    private readonly IComponentVerifyTicketStore _componentVerifyTicketStore;

    public TicketRecordingWeChatThirdPartyPlatformAuthEventHandler(
        IComponentVerifyTicketStore componentVerifyTicketStore)
    {
        _componentVerifyTicketStore = componentVerifyTicketStore;
    }

    public virtual async Task<WeChatEventHandlingResult> HandleAsync(AuthNotificationModel model)
    {
        if (model.ComponentVerifyTicket.IsNullOrWhiteSpace())
        {
            return new WeChatEventHandlingResult(false, "缺少 ComponentVerifyTicket");
        }

        await _componentVerifyTicketStore.SetAsync(model.AppId, model.ComponentVerifyTicket);

        return new WeChatEventHandlingResult(true);
    }
}