using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;

public interface IWeChatThirdPartyPlatformOptionsProvider
{
    Task<IWeChatThirdPartyPlatformOptions> GetAsync([CanBeNull] string appId);
}