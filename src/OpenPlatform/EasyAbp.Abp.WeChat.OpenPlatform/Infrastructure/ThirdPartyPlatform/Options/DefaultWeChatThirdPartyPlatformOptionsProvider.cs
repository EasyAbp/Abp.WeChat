using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;

[Dependency(TryRegister = true)]
public class DefaultWeChatThirdPartyPlatformOptionsProvider : IWeChatThirdPartyPlatformOptionsProvider,
    ITransientDependency
{
    private readonly AbpWeChatThirdPartyPlatformOptions _options;

    public DefaultWeChatThirdPartyPlatformOptionsProvider(IOptions<AbpWeChatThirdPartyPlatformOptions> options)
    {
        _options = options.Value;
    }

    public virtual async Task<IWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        if (_options.AppId != appId)
        {
            throw new UserFriendlyException("请实现 IWeChatThirdPartyPlatformOptionsProvider，以支持多 appid 场景");
        }

        return _options;
    }
}