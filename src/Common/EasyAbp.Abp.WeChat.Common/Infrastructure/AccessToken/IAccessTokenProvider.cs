using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAsync(string appId, string appSecret);
    }
}