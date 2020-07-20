using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync(string appId, string appSecret);
    }
}