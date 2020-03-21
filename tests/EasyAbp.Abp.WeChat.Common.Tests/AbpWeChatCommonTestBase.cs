using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace EasyAbp.Abp.WeChat.Common.Tests
{
    public class AbpWeChatCommonTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}