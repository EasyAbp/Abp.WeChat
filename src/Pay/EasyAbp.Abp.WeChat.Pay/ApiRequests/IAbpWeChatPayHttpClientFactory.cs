using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public interface IAbpWeChatPayHttpClientFactory
{
    Task<HttpClient> CreateAsync([CanBeNull] string mchId);
}