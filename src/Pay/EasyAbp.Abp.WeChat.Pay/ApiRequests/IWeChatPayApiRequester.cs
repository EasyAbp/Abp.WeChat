using System.Threading.Tasks;
using System.Xml;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    public interface IWeChatPayApiRequester
    {
        Task<XmlDocument> RequestAsync(string url, string body);
    }
}