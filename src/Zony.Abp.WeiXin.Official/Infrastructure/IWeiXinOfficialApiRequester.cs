using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeiXin.Official.Services.RequestDtos;

namespace Zony.Abp.WeiXin.Official.Infrastructure
{
    public interface IWeiXinOfficialApiRequester : ITransientDependency
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method,IOfficialRequest officialRequest);
    }
}