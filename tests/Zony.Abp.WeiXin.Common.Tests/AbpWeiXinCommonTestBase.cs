using Volo.Abp;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeiXin.Common.Tests
{
    public class AbpWeiXinCommonTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}