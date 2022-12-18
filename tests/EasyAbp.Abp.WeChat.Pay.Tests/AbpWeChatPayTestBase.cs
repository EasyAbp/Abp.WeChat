using Microsoft.Extensions.Options;
using EasyAbp.Abp.WeChat.Common.Tests;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services;

namespace EasyAbp.Abp.WeChat.Pay.Tests
{
    public class AbpWeChatPayTestBase : AbpWeChatCommonTestBase<AbpWeChatPayTestsModule>
    {
        protected AbpWeChatPayOptions AbpWeChatPayOptions { get; }

        protected IAbpWeChatPayServiceFactory WeChatPayServiceFactory { get; }

        public AbpWeChatPayTestBase()
        {
            AbpWeChatPayOptions = GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;
            WeChatPayServiceFactory = GetRequiredService<IAbpWeChatPayServiceFactory>();
        }
    }
}