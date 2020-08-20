using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;

namespace EasyAbp.Abp.WeChat.Pay.Tests
{
    [DependsOn(typeof(AbpWeChatCommonTestsModule),
        typeof(AbpWeChatPayModule),
        typeof(AbpBlobStoringFileSystemModule))]
    public class AbpWeChatPayTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatPayOptions>(op =>
            {
                // TODO: 测试的时候，请在此处填写相关的配置参数。
                op.MchId = "";
                op.ApiKey = "abcdefg";
                // op.CertificateBlobContainerName = "";
                op.CertificateBlobName = "CertificateName";
                op.CertificateSecret = "";
                op.NotifyUrl = "";
                op.RefundNotifyUrl = "RefundNotifyUrl";
            });

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container => { container.UseFileSystem(fileSystem => { fileSystem.BasePath = "/Users/zony/Work/TempFiles"; }); });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var container = context.ServiceProvider.GetRequiredService<IBlobContainer>();
            container.SaveAsync("CertificateName", new byte[] {0x01, 0x02}, true).GetAwaiter().GetResult();
        }
    }
}