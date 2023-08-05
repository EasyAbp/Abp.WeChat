using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public abstract class WeChatThirdPartyPlatformAuthEventHandlerBase<THandler> : IWeChatThirdPartyPlatformAuthEventHandler
    where THandler : IWeChatThirdPartyPlatformAuthEventHandler
{
    public abstract string InfoType { get; }

    public Type HandlerType => typeof(THandler);

    public abstract Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model);
}