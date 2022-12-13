using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;

public interface IWeChatThirdPartyPlatformEventHandlingService
{
    Task<WeChatEventHandlingResult> NotifyAuthAsync(WeChatEventNotificationRequestModel request);

    Task<WeChatEventHandlingResult> NotifyAppAsync([CanBeNull] string componentAppId, [NotNull] string appId,
        WeChatEventNotificationRequestModel request);
}