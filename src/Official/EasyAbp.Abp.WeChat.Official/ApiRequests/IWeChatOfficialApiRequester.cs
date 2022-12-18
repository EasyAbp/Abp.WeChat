using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official.Models;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.ApiRequests
{
    public interface IWeChatOfficialApiRequester
    {
        Task<string> RequestAsync(string targetUrl,
            HttpMethod method,
            [CanBeNull] IOfficialRequest officialRequest,
            [CanBeNull] IAbpWeChatOptions abpWeChatOptions);

        Task<TResponse> RequestAsync<TResponse>(string targetUrl,
            HttpMethod method,
            [CanBeNull] IOfficialRequest officialRequest,
            [CanBeNull] IAbpWeChatOptions abpWeChatOptions);

        Task<TResponse> RequestFromDataAsync<TResponse>(string targetUrl,
            MultipartFormDataContent formDataContent,
            [CanBeNull] IOfficialRequest officialRequest,
            [CanBeNull] IAbpWeChatOptions abpWeChatOptions);
    }
}