using System.Threading.Tasks;
using System.Xml;

namespace Zony.Abp.WeChat.Pay.Infrastructure
{
    public interface IWeChatPayHandler
    {
        Task HandleAsync(XmlDocument xmlDocument);
    }
}