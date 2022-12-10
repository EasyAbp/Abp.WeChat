using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve.Contributors;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(typeof(AbpWeChatCommonModule))]
public class AbpWeChatOpenPlatformModule : AbpModule 
{
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatOpenPlatformResolveOptions>(options =>
        {
            if (!options.WeChatOpenPlatformOptionsResolveContributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
            {
                options.WeChatOpenPlatformOptionsResolveContributors.Insert(0, new AsyncLocalOptionsResolveContributor());
            }
        });
    }
}