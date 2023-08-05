using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

/// <summary>
/// 授权事件通知处理者
/// </summary>
public interface IWeChatThirdPartyPlatformAuthEventHandler
{
    /// <summary>
    /// 仅处理此 InfoType 的事件，有效值参考 <see cref="WeChatThirdPartyPlatformAuthEventInfoTypes"/>
    /// </summary>
    string InfoType { get; }

    /// <summary>
    /// 实现 IWeChatThirdPartyPlatformAuthEventHandler 的类型，用于实例化
    /// </summary>
    Type HandlerType { get; }

    Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model);
}