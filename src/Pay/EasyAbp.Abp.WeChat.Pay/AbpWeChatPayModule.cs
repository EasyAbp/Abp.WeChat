using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Pay.Infrastructure;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.Handlers;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatPayModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpWeChatPayResolveOptions>(options =>
            {
                if (!options.Contributors.Exists(x => x.Name == ConfigurationOptionsResolveContributor.ContributorName))
                {
                    options.Contributors.Add(new ConfigurationOptionsResolveContributor());
                }

                if (!options.Contributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
                {
                    options.Contributors.Insert(0, new AsyncLocalOptionsResolveContributor());
                }
            });
            
            context.Services.AddHttpClient("WeChatPay").ConfigurePrimaryHttpMessageHandler(builder =>
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };

                var options = AsyncHelper.RunSync(() =>
                    builder.GetRequiredService<IWeChatPayOptionsResolver>().ResolveAsync());

                if (string.IsNullOrEmpty(options.CertificatePath)) return handler;
                if (!File.Exists(options.CertificatePath)) throw new FileNotFoundException("指定的证书路径无效，请重新指定有效的证书文件路径。");

                handler.ClientCertificates.Add(new X509Certificate2(options.CertificatePath, options.CertificateSecret,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
                handler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;

                return handler;
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IWeChatPayHandler, SignVerifyHandler>();
        }
    }
}