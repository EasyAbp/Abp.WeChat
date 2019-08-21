using Volo.Abp.Modularity;
using Zony.Abp.WeiXin.Common.Tests;

namespace Zony.Abp.WeiXin.Official.Tests
{
    [DependsOn(typeof(AbpWeiXinCommonTestsModule),
        typeof(AbpWeiXinOfficialModule))]
    public class AbpWeiXinOfficialTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeiXinOfficialOptions>(op =>
            {
                op.AppId = AbpWeiXinOfficialTestsConsts.AppId;
                op.AppSecret = AbpWeiXinOfficialTestsConsts.AppSecret;
            });
        }
    }
}