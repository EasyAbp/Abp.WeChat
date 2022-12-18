using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;

public interface IAuthorizerAccessTokenCache
{
    Task<string> GetOrNullAsync([NotNull] string componentAppId, [NotNull] string authorizerAppId);

    Task SetAsync([NotNull] string componentAppId, [NotNull] string authorizerAppId, [NotNull] string accessToken);
}