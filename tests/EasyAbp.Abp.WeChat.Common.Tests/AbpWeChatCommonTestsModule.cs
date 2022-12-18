using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Common.Tests
{
    [DependsOn(
        typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule),
        typeof(AbpWeChatCommonModule)
    )]
    public class AbpWeChatCommonTestsModule : AbpModule
    {
        
    }
}