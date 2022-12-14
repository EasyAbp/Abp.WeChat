using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving.Contributors;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(
    typeof(AbpWeChatCommonModule),
    typeof(AbpWeChatOpenPlatformAbstractionsModule)
)]
public class AbpWeChatOpenPlatformModule : AbpModule 
{
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatThirdPartyPlatformResolvingOptions>(options =>
        {
            if (!options.WeChatThirdPartyPlatformOptionsResolveContributors.Exists(x => x.Name == AsyncLocalOptionsResolvingContributor.ContributorName))
            {
                options.WeChatThirdPartyPlatformOptionsResolveContributors.Insert(0, new AsyncLocalOptionsResolvingContributor());
            }
        });
    }
}