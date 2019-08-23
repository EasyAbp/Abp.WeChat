using System.Threading.Tasks;
using System.Xml;

namespace Zony.Abp.WeiXin.Pay.Infrastructure
{
    public interface IWeChatPayApiRequester
    {
        Task<XmlDocument> RequestAsync(string url, string body);
    }
}