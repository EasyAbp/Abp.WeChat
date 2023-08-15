using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    /// <summary>
    /// 定义了微信支付 V3 接口的请求器。
    /// </summary>
    public interface IWeChatPayApiRequester
    {
        Task<string> RequestAsync(HttpMethod method, string url, [CanBeNull] string body = null, [CanBeNull] string mchId = null);

        Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, [CanBeNull] string body = null, [CanBeNull] string mchId = null);

        Task<string> RequestAsync(HttpMethod method, string url, [NotNull] object body, [CanBeNull] string mchId = null);

        Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, [NotNull] object body, [CanBeNull] string mchId = null);
    }
}