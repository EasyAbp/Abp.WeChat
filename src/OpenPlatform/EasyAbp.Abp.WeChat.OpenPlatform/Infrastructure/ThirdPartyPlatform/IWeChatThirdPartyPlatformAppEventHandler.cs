using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;

/// <summary>
/// 微信应用事件通知处理者
/// </summary>
public interface IWeChatThirdPartyPlatformAppEventHandler
{
    /// <summary>
    /// 仅处理回调请求中，相应的 Event 值的事件
    /// </summary>
    public string Event { get; }

    Task HandleAsync(WeChatThirdPartyPlatformAppEventHandlerContext context);
}