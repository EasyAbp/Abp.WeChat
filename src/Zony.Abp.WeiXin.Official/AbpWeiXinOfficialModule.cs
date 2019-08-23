using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common;

namespace Zony.Abp.WeiXin.Official
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeiXinOfficialModule : AbpModule 
    {
        
    }
}