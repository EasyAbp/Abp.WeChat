using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;

namespace EasyAbp.Abp.WeChat.Official
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatOfficialModule : AbpModule 
    {
        
    }
}