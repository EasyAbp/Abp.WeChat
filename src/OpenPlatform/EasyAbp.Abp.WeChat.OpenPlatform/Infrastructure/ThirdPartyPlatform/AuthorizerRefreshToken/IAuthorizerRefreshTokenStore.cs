using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AuthorizerRefreshToken;

public interface IAuthorizerRefreshTokenStore
{
    Task<string> GetOrNullAsync(string authorizerAppId);

    Task SetAsync(string authorizerAppId, string authorizerRefreshToken);
}