using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeChat.Common.Tests
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule))]
    public class AbpWeChatCommonTestsModule : AbpModule
    {
        
    }
}