using EasyAbp.Abp.WeChat.Common.Tests;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests
{
    [DependsOn(typeof(AbpWeChatCommonTestsModule),
        typeof(AbpWeChatMiniProgramModule))]
    public class AbpWeChatMiniProgramTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatMiniProgramOptions>(op =>
            {
                op.AppId = AbpWeChatMiniProgramTestsConsts.AppId;
                op.AppSecret = AbpWeChatMiniProgramTestsConsts.AppSecret;
            });
        }
    }
}