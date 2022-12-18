using System.IO;
using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;
using EasyAbp.Abp.WeChat.Pay.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Threading;

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
                op.MchId = "10000100";
                op.ApiKey = "abcdefg";
                // op.CertificateBlobContainerName = "";
                op.CertificateBlobName = "CertificateName";
                op.CertificateSecret = "";
                op.NotifyUrl = "";
                op.RefundNotifyUrl = "RefundNotifyUrl";
            });

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "test-temp-files");
                    });
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var container = context.ServiceProvider.GetRequiredService<IBlobContainer>();

            if (!container.ExistsAsync("CertificateName").GetAwaiter().GetResult())
            {
                AsyncHelper.RunSync(() => container.SaveAsync("CertificateName", new byte[] { 0x01, 0x02 }, true));
            }
        }
    }
}