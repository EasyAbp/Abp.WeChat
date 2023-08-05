using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public abstract class WeChatOfficialAppEventHandlerBase<THandler> : IWeChatOfficialAppEventHandler
    where THandler : IWeChatOfficialAppEventHandler
{
    public abstract string MsgType { get; }

    public abstract int Priority { get; }

    public Type HandlerType => typeof(THandler);

    public abstract Task<AppEventHandlingResult> HandleAsync(string appId, WeChatAppEventModel model);
}