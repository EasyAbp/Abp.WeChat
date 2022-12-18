using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;

public interface IWeChatThirdPartyPlatformApiRequester
{
    Task<string> RequestAsync(
        string targetUrl,
        HttpMethod method,
        [CanBeNull] IOpenPlatformRequest openPlatformRequest,
        [CanBeNull] IAbpWeChatOptions abpWeChatOptions);

    Task<TResponse> RequestAsync<TResponse>(
        string targetUrl,
        HttpMethod method,
        [CanBeNull] IOpenPlatformRequest openPlatformRequest,
        [CanBeNull] IAbpWeChatOptions abpWeChatOptions);

    Task<TResponse> RequestFromDataAsync<TResponse>(
        string targetUrl,
        MultipartFormDataContent formDataContent,
        [CanBeNull] IOpenPlatformRequest openPlatformRequest,
        [CanBeNull] IAbpWeChatOptions abpWeChatOptions);
}