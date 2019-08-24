using Volo.Abp;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeChat.Common.Tests
{
    public class AbpWeChatCommonTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}