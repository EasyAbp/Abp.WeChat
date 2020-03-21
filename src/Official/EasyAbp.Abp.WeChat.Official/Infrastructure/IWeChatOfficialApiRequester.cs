using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public interface IWeChatOfficialApiRequester : ITransientDependency
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IOfficialRequest officialRequest = null);
    }
}