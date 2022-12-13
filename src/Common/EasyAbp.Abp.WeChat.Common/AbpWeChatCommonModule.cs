using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Common
{
    [DependsOn(
        typeof(AbpCachingModule),
        typeof(AbpWeChatCommonAbstractionsModule)
)]
    public class AbpWeChatCommonModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
        }
    }
}