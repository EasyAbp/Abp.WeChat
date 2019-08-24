using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common.Tests;

namespace Zony.Abp.WeChat.Official.Tests
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