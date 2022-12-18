using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ComponentAccessToken;

public interface IComponentAccessTokenProvider
{
    Task<string> GetAsync(string componentAppId, string componentAppSecret);
}