using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.Official.HttpApi
{
    [DependsOn(typeof(AbpWeChatOfficialModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpWeChatOfficialHttpApiModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            VerifyOptions(context);
        }

        private void VerifyOptions(ApplicationInitializationContext context)
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                var options = context.ServiceProvider.GetRequiredService<IWeChatOfficialOptionsResolver>().Resolve();
                if (string.IsNullOrEmpty(options.Token)) throw new ArgumentNullException(nameof(options.Token), "该参数是微信公众平台的必填参数，不能够为空。");
                if (string.IsNullOrEmpty(options.AppId)) throw new ArgumentNullException(nameof(options.AppId), "该参数是微信公众平台的必填参数，不能够为空。");
                if (string.IsNullOrEmpty(options.AppSecret)) throw new ArgumentNullException(nameof(options.AppSecret), "该参数是微信公众平台的必填参数，不能够为空。");
            }
        }
    }
}