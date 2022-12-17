using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors;

namespace EasyAbp.Abp.WeChat.MiniProgram
{
    [DependsOn(
        typeof(AbpWeChatCommonModule),
        typeof(AbpWeChatMiniProgramAbstractionsModule)
    )]
    public class AbpWeChatMiniProgramModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatMiniProgramResolveOptions>(options =>
            {
                if (!options.Contributors.Exists(x => x.Name == ConfigurationOptionsResolveContributor.ContributorName))
                {
                    options.Contributors.Add(new ConfigurationOptionsResolveContributor());
                }

                if (!options.Contributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
                {
                    options.Contributors.Insert(0, new AsyncLocalOptionsResolveContributor());
                }
            });
        }
    }
}