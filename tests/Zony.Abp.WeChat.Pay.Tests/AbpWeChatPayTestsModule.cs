using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common.Tests;

namespace Zony.Abp.WeChat.Pay.Tests
{
    [DependsOn(typeof(AbpWeChatCommonTestsModule),
        typeof(AbpWeChatPayModule))]
    public class AbpWeChatPayTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatPayOptions>(op =>
            {
                // op.ApiKey = "e566e27045551d3a3806887497a15f86";
                // op.IsSandBox = true;
                // op.NotifyUrl = "Null";

                op.ApiKey = "";
                op.CertificatePath = "";
                op.CertificateSecret = "";
            });
        }
    }
}