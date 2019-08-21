using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeiXin.Common
{
    [DependsOn(typeof(AbpCachingModule))]
    public class AbpWeiXinCommonModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
        }
    }
}