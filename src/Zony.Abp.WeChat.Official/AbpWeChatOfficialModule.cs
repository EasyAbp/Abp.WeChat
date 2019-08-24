using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common;

namespace Zony.Abp.WeChat.Official
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatOfficialModule : AbpModule 
    {
        
    }
}