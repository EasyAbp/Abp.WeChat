using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;

namespace EasyAbp.Abp.WeChat.Official.Tests
{
    [DependsOn(typeof(AbpWeChatCommonTestsModule),
        typeof(AbpWeChatOfficialModule))]
    public class AbpWeChatOfficialTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatOfficialOptions>(op =>
            {
                op.AppId = AbpWeChatOfficialTestsConsts.AppId;
                op.AppSecret = AbpWeChatOfficialTestsConsts.AppSecret;
            });
        }
    }
}