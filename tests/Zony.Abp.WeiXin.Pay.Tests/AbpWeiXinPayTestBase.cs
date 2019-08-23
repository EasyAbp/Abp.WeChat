using Microsoft.Extensions.Options;
using Zony.Abp.WeiXin.Common.Tests;

namespace Zony.Abp.WeiXin.Pay.Tests
{
    public class AbpWeiXinPayTestBase : AbpWeiXinCommonTestBase<AbpWeiXinPayTestsModule>
    {
        protected AbpWeiXinPayOptions AbpWeiXinPayOptions { get; }

        public AbpWeiXinPayTestBase()
        {
            AbpWeiXinPayOptions = GetRequiredService<IOptions<AbpWeiXinPayOptions>>().Value;
        }
    }
}