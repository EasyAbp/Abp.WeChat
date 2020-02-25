using Volo.Abp.Modularity;
using Zony.Abp.WeChat.Common;

namespace Zony.Abp.WeChat.MiniProgram
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatMiniProgram : AbpModule
    {
    }
}