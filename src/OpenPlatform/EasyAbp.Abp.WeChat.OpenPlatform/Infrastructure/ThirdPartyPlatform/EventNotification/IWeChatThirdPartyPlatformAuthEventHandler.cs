using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;

/// <summary>
/// 授权事件通知处理者
/// </summary>
public interface IWeChatThirdPartyPlatformAuthEventHandler
{
    /// <summary>
    /// 仅处理此 InfoType 的事件，有效值参考 <see cref="WeChatThirdPartyPlatformAuthEventInfoTypes"/>
    /// </summary>
    public string InfoType { get; }

    Task<WeChatEventHandlingResult> HandleAsync(AuthNotificationModel model);
}