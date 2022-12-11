using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure;

public interface IWeChatOpenPlatformApiRequester
{
    Task<string> RequestAsync(string targetUrl,
        HttpMethod method,
        IOpenPlatformRequest openPlatformRequest = null);

    Task<TResponse> RequestAsync<TResponse>(string targetUrl,
        HttpMethod method,
        IOpenPlatformRequest openPlatformRequest = null);

    Task<TResponse> RequestFromDataAsync<TResponse>(string targetUrl,
        MultipartFormDataContent formDataContent,
        IOpenPlatformRequest openPlatformRequest = null);
}