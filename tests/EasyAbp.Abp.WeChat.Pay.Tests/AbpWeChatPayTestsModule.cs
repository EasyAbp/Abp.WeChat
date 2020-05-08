using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;

namespace EasyAbp.Abp.WeChat.Pay.Tests
{
    [DependsOn(typeof(AbpWeChatCommonTestsModule),
        typeof(AbpWeChatPayModule))]
    public class AbpWeChatPayTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatPayOptions>(op =>
            {
                // TODO: 测试的时候，请在此处填写相关的配置参数。
                op.MchId = "";
                op.ApiKey = "";
                op.CertificatePath = "";
                op.CertificateSecret = "";
                op.NotifyUrl = "";
            });
        }
    }
}