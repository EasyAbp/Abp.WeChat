using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace EasyAbp.Abp.WeChat.Common.Tests
{
    public class AbpWeChatCommonTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected IAbpWeChatServiceFactory WeChatServiceFactory { get; }

        public AbpWeChatCommonTestBase()
        {
            WeChatServiceFactory = GetRequiredService<IAbpWeChatServiceFactory>();
        }

        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}