using Microsoft.Extensions.Options;
using Zony.Abp.WeiXin.Common.Tests;

namespace Zony.Abp.WeChat.Pay.Tests
{
    public class AbpWeChatPayTestBase : AbpWeiXinCommonTestBase<AbpWeChatPayTestsModule>
    {
        protected AbpWeChatPayOptions AbpWeChatPayOptions { get; }

        public AbpWeChatPayTestBase()
        {
            AbpWeChatPayOptions = GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;
        }
    }
}