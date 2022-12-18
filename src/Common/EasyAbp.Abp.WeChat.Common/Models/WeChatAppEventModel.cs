using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.Common.Models;

/// <summary>
/// 微信应用通用事件通知模型
/// 参考微信公众号文档：https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Receiving_event_pushes.html
/// </summary>
public class WeChatAppEventModel : ExtensibleObject
{
    /// <summary>
    /// 开发者微信号
    /// 一般是指：公众号/小程序的原始ID
    /// </summary>
    public string ToUserName => this.GetProperty<string>("ToUserName");

    /// <summary>
    /// 发送方帐号（一个OpenID）
    /// </summary>
    public string FromUserName => this.GetProperty<string>("FromUserName");

    /// <summary>
    /// 消息创建时间 （整型）
    /// </summary>
    public int CreateTime => this.GetProperty<int>("CreateTime");

    /// <summary>
    /// 消息类型，event
    /// </summary>
    public string MsgType => this.GetProperty<string>("MsgType");

    /// <summary>
    /// 事件类型，例如：subscribe, funds_order_refund
    /// </summary>
    public string Event => this.GetProperty<string>("Event");
}