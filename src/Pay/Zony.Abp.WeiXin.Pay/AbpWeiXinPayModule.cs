using Volo.Abp.Modularity;
using Zony.Abp.WeiXin.Common;

namespace Zony.Abp.WeiXin.Pay
{
    [DependsOn(typeof(AbpWeiXinCommonModule))]
    public class AbpWeiXinPayModule : AbpModule
    {
        
    }
}