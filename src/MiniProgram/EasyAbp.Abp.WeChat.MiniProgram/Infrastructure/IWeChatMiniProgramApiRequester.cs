using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public interface IWeChatMiniProgramApiRequester
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IMiniProgramRequest miniProgramRequest = null, bool withAccessToken = true);
        
        Task<TResponse> RequestGetBinaryDataAsync<TResponse>(string targetUrl, HttpMethod method, IMiniProgramRequest miniProgramRequest = null, bool withAccessToken = true) where TResponse : IHasBinaryData;
    }
}