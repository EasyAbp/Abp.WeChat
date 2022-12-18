using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AuthorizerRefreshToken;

public interface IAuthorizerRefreshTokenStore
{
    Task<string> GetOrNullAsync(string componentAppId, string authorizerAppId);

    Task SetAsync(string componentAppId, string authorizerAppId, string authorizerRefreshToken);
}