using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving.Contributors;

public class ConfigurationOptionsResolvingContributor : IWeChatThirdPartyPlatformOptionsResolvingContributor
{
    public const string ContributorName = "Configuration";
    public string Name => ContributorName;

    public void Resolve(WeChatThirdPartyPlatformOptionsResolvingContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatThirdPartyPlatformOptions>>().Value;
    }

    public ValueTask ResolveAsync(WeChatThirdPartyPlatformOptionsResolvingContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatThirdPartyPlatformOptions>>().Value;

        return new ValueTask();
    }
}