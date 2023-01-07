using System.Threading.Tasks;
using System.Xml;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    public interface IWeChatPayApiRequester
    {
        Task<XmlDocument> RequestAsync([NotNull] string url, [NotNull] string body, [CanBeNull] string mchId);
    }
}