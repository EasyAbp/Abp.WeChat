using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public interface IWeChatPayEventRequestHandlingService
{
    Task<WeChatRequestHandlingResult> NotifyAsync([CanBeNull] string mchId, XmlDocument xmlDocument);

    Task<WeChatRequestHandlingResult> RefundNotifyAsync([CanBeNull] string mchId, XmlDocument xmlDocument);
}