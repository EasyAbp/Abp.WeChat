using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors;

namespace EasyAbp.Abp.WeChat.Official
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatOfficialModule : AbpModule 
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatOfficialResolveOptions>(options =>
            {
                if (!options.WeChatOfficialOptionsResolveContributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
                {
                    options.WeChatOfficialOptionsResolveContributors.Insert(0, new AsyncLocalOptionsResolveContributor());
                }
            });
        }
    }
}