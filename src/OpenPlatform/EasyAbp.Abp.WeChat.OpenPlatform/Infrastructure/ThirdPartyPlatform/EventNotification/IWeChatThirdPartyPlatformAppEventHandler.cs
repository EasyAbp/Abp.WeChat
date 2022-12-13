using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;

/// <summary>
/// 微信应用事件通知处理者
/// </summary>
public interface IWeChatThirdPartyPlatformAppEventHandler
{
    /// <summary>
    /// 仅处理回调请求中，相应的 Event 值的事件
    /// </summary>
    public string Event { get; }

    /// <summary>
    /// 事件处理实现
    /// 请注意，此方法中如有需要调用微信 API，请在第一行使用：
    /// using var changeOptions = WeChatXxxxAsyncLocal.Change(new AbpWeChatXxxxOptions { AppId = context.AuthorizerAppId; })
    /// 不要设置 AppSecret，此时会自动使用微信第三方平台的 authorizer_access_token
    /// 见 <see cref="HybridAccessTokenProvider"/>
    /// </summary>
    Task<WeChatEventHandlingResult> HandleAsync(
        [CanBeNull] string componentAppId, [NotNull] string appId, WeChatAppNotificationModel model);
}