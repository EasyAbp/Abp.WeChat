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
                const string mchId = AbpWeChatPayTestConsts.MchId;

                op.MchId = mchId;
                op.ApiV3Key = AbpWeChatPayTestConsts.ApiKey;
                // op.CertificateBlobContainerName = "";
                op.CertificateBlobName = "apiclient_cert.p12";
                op.CertificateSecret = mchId;
                op.NotifyUrl = $"https://my-abp-app.io/wechat-pay/notify/mch-id/{mchId}";
                op.RefundNotifyUrl = $"https://my-abp-app.io/wechat-pay/refund-notify/mch-id/{mchId}";
                op.IsSandBox = AbpWeChatPayTestConsts.IsSandBox;
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