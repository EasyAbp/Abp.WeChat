using Volo.Abp.Modularity;
using Zony.Abp.WeiXin.Common.Tests;

namespace Zony.Abp.WeiXin.Pay.Tests
{
    [DependsOn(typeof(AbpWeiXinCommonTestsModule),
        typeof(AbpWeiXinPayModule))]
    public class AbpWeiXinPayTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeiXinPayOptions>(op =>
            {
                op.ApiKey = "e566e27045551d3a3806887497a15f86";
                op.IsSandBox = true;
                op.NotifyUrl = "https://www.baidu.com";
            });
        }
    }
}