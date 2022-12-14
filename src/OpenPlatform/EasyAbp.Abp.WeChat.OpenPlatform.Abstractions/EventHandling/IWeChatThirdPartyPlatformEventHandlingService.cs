using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;

public interface IWeChatThirdPartyPlatformEventHandlingService
{
    Task<WeChatEventHandlingResult> NotifyAuthAsync([CanBeNull] string componentAppId,
        WeChatEventNotificationRequestModel request);

    Task<WeChatEventHandlingResult> NotifyAppAsync([CanBeNull] string componentAppId, [NotNull] string authorizerAppId,
        WeChatEventNotificationRequestModel request);
}