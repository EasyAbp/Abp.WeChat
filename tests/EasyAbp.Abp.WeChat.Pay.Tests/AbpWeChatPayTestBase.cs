using Microsoft.Extensions.Options;
using EasyAbp.Abp.WeChat.Common.Tests;

namespace EasyAbp.Abp.WeChat.Pay.Tests
{
    public class AbpWeChatPayTestBase : AbpWeChatCommonTestBase<AbpWeChatPayTestsModule>
    {
        protected AbpWeChatPayOptions AbpWeChatPayOptions { get; }

        public AbpWeChatPayTestBase()
        {
            AbpWeChatPayOptions = GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;
        }
    }
}