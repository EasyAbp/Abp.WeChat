using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Handlers;

public class TicketRecordingWeChatThirdPartyPlatformAuthEventHandler : IWeChatThirdPartyPlatformAuthEventHandler
{
    public string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.ComponentVerifyTicket;

    private readonly IComponentVerifyTicketStore _componentVerifyTicketStore;

    public TicketRecordingWeChatThirdPartyPlatformAuthEventHandler(
        IComponentVerifyTicketStore componentVerifyTicketStore)
    {
        _componentVerifyTicketStore = componentVerifyTicketStore;
    }

    public virtual async Task HandleAsync(WeChatThirdPartyPlatformAuthEventHandlerContext context)
    {
        if (context.Model.ComponentVerifyTicket.IsNullOrWhiteSpace())
        {
            return;
        }

        await _componentVerifyTicketStore.SetAsync(context.Model.AppId, context.Model.ComponentVerifyTicket);
    }
}