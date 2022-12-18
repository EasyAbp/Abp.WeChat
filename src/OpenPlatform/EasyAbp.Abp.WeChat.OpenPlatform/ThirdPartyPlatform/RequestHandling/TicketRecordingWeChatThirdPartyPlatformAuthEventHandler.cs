using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.VerifyTicket;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

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

    public virtual async Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model)
    {
        if (model.ComponentVerifyTicket.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("缺少 ComponentVerifyTicket，通知消息无效。AppId：{0}", model.AppId);

            return new WeChatRequestHandlingResult(false);
        }

        try
        {
            await _componentVerifyTicketStore.SetAsync(model.AppId, model.ComponentVerifyTicket);
        }
        catch
        {
            _logger.LogWarning(
                "ComponentVerifyTicketStore 保存出错，导致，导致 ComponentVerifyTicket 设置失败。AppId：{0}", model.AppId);

            return new WeChatRequestHandlingResult(false);
        }

        return new WeChatRequestHandlingResult(true);
    }
}