using System;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatPayModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient("WeChatPay").ConfigurePrimaryHttpMessageHandler(builder =>
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };

                var options = builder.GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;

                if (string.IsNullOrEmpty(options.CertificatePath)) return handler;
                if (!File.Exists(options.CertificatePath)) throw new FileNotFoundException("指定的证书路径无效，请重新指定有效的证书文件路径。");

                handler.ClientCertificates.Add(new X509Certificate2(options.CertificatePath, options.CertificateSecret,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
                handler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;

                return handler;
            });
        }
    }
}