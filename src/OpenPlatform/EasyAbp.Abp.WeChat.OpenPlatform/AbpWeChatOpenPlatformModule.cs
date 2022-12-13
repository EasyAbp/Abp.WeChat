using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve.Contributors;
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
        Configure<AbpWeChatThirdPartyPlatformResolveOptions>(options =>
        {
            if (!options.WeChatThirdPartyPlatformOptionsResolveContributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
            {
                options.WeChatThirdPartyPlatformOptionsResolveContributors.Insert(0, new AsyncLocalOptionsResolveContributor());
            }
        });
    }
}