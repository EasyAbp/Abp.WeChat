using Volo.Abp;
using Zony.Abp.WeiXin.Common.Tests;

namespace Zony.Abp.WeiXin.Official.Tests
{
    public class AbpWeiXinOfficialTestBase : AbpWeiXinCommonTestBase<AbpWeiXinOfficialTestsModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}