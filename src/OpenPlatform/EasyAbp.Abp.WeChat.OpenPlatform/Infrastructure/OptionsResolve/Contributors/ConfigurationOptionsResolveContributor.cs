using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve.Contributors;

public class ConfigurationOptionsResolveContributor : IWeChatOpenPlatformOptionsResolveContributor
{
    public const string ContributorName = "Configuration";
    public string Name => ContributorName;

    public void Resolve(WeChatOpenPlatformResolveContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatOpenPlatformOptions>>().Value;
    }

    public ValueTask ResolveAsync(WeChatOpenPlatformResolveContext context)
    {
        context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatOpenPlatformOptions>>().Value;

        return new ValueTask();
    }
}