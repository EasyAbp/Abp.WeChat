using System.Threading.Tasks;
using System.Xml;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    /// <summary>
    /// 定义了微信支付回调处理器。
    /// </summary>
    public interface IWeChatPayHandler
    {
        Task HandleAsync(XmlDocument xmlDocument);
    }
}