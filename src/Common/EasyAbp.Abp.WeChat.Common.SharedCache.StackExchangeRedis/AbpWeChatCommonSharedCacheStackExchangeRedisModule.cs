using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Localization;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis
{
    [DependsOn(
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpWeChatCommonModule))]
    public class AbpWeChatCommonSharedCacheStackExchangeRedisModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpWeChatCommonSharedCacheStackExchangeRedisModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SharedCacheStackExchangeRedisResource>("en")
                    .AddVirtualJson("/Localization/StackExchangeRedis");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis", typeof(SharedCacheStackExchangeRedisResource));
            });
        }
    }
}