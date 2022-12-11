using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.ComponentAccessToken;

public interface IComponentAccessTokenProvider
{
    Task<string> GetAsync(string componentAppId, string componentAppSecret);
}