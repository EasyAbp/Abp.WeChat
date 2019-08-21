using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Zony.Abp.WeiXin.Common.Tests
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule))]
    public class AbpWeiXinCommonTestsModule : AbpModule
    {
        
    }
}