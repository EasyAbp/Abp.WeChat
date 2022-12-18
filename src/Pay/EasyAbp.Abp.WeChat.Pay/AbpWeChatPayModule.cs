using System;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Pay.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(
        typeof(AbpWeChatCommonModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpWeChatPayAbstractionsModule)
    )]
    public class AbpWeChatPayModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureWeChatPayHttpClient(context);
        }

        private void ConfigureWeChatPayHttpClient(ServiceConfigurationContext context)
        {
            // todo: 证书需支持多商户
            context.Services.AddHttpClient("WeChatPay").ConfigurePrimaryHttpMessageHandler(builder =>
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };

                var options = AsyncHelper.RunSync(() => builder.GetRequiredService<IAbpWeChatPayOptionsProvider>().GetAsync(null));
                if (string.IsNullOrEmpty(options.CertificateBlobName)) return handler;

                var blobContainer = options.CertificateBlobContainerName.IsNullOrWhiteSpace()
                    ? builder.GetRequiredService<IBlobContainer>()
                    : builder.GetRequiredService<IBlobContainerFactory>().Create(options.CertificateBlobContainerName);
                
                var certificateBytes = AsyncHelper.RunSync(() => blobContainer.GetAllBytesOrNullAsync(options.CertificateBlobName));
                if (certificateBytes == null) throw new FileNotFoundException("指定的证书路径无效，请重新指定有效的证书文件路径。");

                handler.ClientCertificates.Add(new X509Certificate2(
                    certificateBytes,
                    options.CertificateSecret,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
                handler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;

                return handler;
            });
        }
    }
}