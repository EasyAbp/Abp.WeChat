using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common;

namespace Zony.Abp.WeChat.Pay
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatPayModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;

            if (string.IsNullOrEmpty(options.NotifyUrl)) throw new ArgumentNullException("请指定有效的支付回调地址。");
            if (string.IsNullOrEmpty(options.ApiKey)) throw new ArgumentNullException("请指定有效的微信 API 密钥。");
        }
    }
}