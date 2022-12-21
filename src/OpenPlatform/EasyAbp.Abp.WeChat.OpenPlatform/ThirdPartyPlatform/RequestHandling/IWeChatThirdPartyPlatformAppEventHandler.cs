using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

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
    /// 事件处理实现。
    /// 请注意，此方法已在外层切换了 <see cref="ICurrentWeChatThirdPartyPlatform"/>，
    /// 因此可以直接使用任意微信的接口服务，第三方平台会提供接口鉴权凭证。
    /// </summary>
    Task<WeChatRequestHandlingResult> HandleAsync(
        [CanBeNull] string componentAppId, [NotNull] string authorizerAppId, WeChatAppEventModel model);
}