using Microsoft.Extensions.Options;
using Zony.Abp.WeChat.Common.Tests;

namespace Zony.Abp.WeChat.Pay.Tests
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