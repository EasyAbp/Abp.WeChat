using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

/// <summary>
/// 微信公众号事件通知处理者
/// </summary>
public interface IWeChatOfficialAppEventHandler
{
    /// <summary>
    /// 仅处理回调请求中，相应的 MsgType 值的事件
    /// </summary>
    public string MsgType { get; }

    /// <summary>
    /// Handler 执行的优先级，值更大的先执行
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// 事件处理实现。
    /// </summary>
    Task<AppEventHandlingResult> HandleAsync([NotNull] string appId, WeChatAppEventModel model);
}