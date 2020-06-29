using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public interface IWeChatMiniProgramApiRequester : ITransientDependency
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IMiniProgramRequest miniProgramRequest = null);
    }
}