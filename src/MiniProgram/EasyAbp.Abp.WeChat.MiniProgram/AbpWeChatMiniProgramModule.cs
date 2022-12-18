using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;

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
        }
    }
}