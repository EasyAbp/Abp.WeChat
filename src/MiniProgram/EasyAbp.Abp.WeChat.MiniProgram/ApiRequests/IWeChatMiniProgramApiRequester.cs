using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.MiniProgram.ApiRequests
{
    public interface IWeChatMiniProgramApiRequester
    {
        Task<TResponse> RequestAsync<TResponse>(
            string targetUrl,
            HttpMethod method,
            [CanBeNull] IMiniProgramRequest miniProgramRequest,
            [CanBeNull] IAbpWeChatOptions abpWeChatOptions);

        Task<TResponse> RequestGetBinaryDataAsync<TResponse>(
            string targetUrl,
            HttpMethod method,
            [CanBeNull] IMiniProgramRequest miniProgramRequest,
            [CanBeNull] IAbpWeChatOptions abpWeChatOptions) where TResponse : IHasBinaryData;
    }
}