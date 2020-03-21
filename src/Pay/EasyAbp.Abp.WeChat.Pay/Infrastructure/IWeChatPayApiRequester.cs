using System.Threading.Tasks;
using System.Xml;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    public interface IWeChatPayApiRequester
    {
        Task<XmlDocument> RequestAsync(string url, string body);
    }
}