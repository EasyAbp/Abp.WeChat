using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

public interface IHttpApiWeChatThirdPartyPlatformOptionsProvider
{
    Task<IWeChatThirdPartyPlatformOptions> GetAsync([CanBeNull] string appId);
}