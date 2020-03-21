using System.Threading.Tasks;
using System.Xml;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    /// <summary>
    /// 定义了微信支付退款回调的处理器。
    /// </summary>
    public interface IWeChatPayRefundHandler
    {
        Task HandleAsync(XmlDocument xmlDocument);
    }
}