using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public interface IWeChatThirdPartyPlatformEventRequestHandlingService
{
    Task<WeChatRequestHandlingResult> NotifyAuthAsync([CanBeNull] string componentAppId, WeChatEventRequestModel request);

    Task<WeChatRequestHandlingResult> NotifyAppAsync([CanBeNull] string componentAppId, [NotNull] string authorizerAppId,
        WeChatEventRequestModel request);
}