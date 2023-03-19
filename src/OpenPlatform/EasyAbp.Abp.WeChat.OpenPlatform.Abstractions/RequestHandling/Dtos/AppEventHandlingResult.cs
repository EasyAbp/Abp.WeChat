using EasyAbp.Abp.WeChat.Common.RequestHandling;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;

public class AppEventHandlingResult : WeChatRequestHandlingResult
{
    /// <summary>
    /// 如果设置此值，将以此值回复微信服务器，否则返回默认的 success。
    /// 参考：https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Passive_user_reply_message.html
    /// 注意1：如果有多个 handler 设置了本值，则 Priority 更小的 handler 生效，即后执行的结果覆盖先执行的结果。
    /// 注意2：如果需要返回特定格式（如 XML），目前需要自行转成文本内容后设置到本值。
    /// </summary>
    [CanBeNull]
    public IResponseToWeChatModel ResponseToWeChatModel { get; set; }

    public AppEventHandlingResult()
    {
    }

    public AppEventHandlingResult(bool success, string failureReason = null) : base(success, failureReason)
    {
    }

    public AppEventHandlingResult([NotNull] IResponseToWeChatModel responseToWeChatModel) : base(true)
    {
        ResponseToWeChatModel = responseToWeChatModel;
    }
}