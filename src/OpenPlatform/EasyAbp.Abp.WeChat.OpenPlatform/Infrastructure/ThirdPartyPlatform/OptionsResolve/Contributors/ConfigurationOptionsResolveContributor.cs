using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve.Contributors;

public class ConfigurationOptionsResolveContributor : IWeChatThirdPartyPlatformOptionsResolveContributor
{
    public const string ContributorName = "Configuration";
    public string Name => ContributorName;

    public void Resolve(WeChatOpenPlatformResolveContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatThirdPartyPlatformOptions>>().Value;
    }

    public ValueTask ResolveAsync(WeChatOpenPlatformResolveContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatThirdPartyPlatformOptions>>().Value;

        return new ValueTask();
    }
}