using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public abstract class WeChatThirdPartyPlatformAppEventHandlerBase<THandler> : IWeChatThirdPartyPlatformAppEventHandler
    where THandler : IWeChatThirdPartyPlatformAppEventHandler
{
    public abstract string MsgType { get; }

    public abstract int Priority { get; }

    public Type HandlerType => typeof(THandler);

    public abstract Task<AppEventHandlingResult> HandleAsync(string componentAppId, string authorizerAppId,
        WeChatAppEventModel model);
}