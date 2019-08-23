using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeChat.Common
{
    [DependsOn(typeof(AbpCachingModule))]
    public class AbpWeChatCommonModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
        }
    }
}