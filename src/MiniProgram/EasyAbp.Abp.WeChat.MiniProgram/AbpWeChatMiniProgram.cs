using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;

namespace EasyAbp.Abp.WeChat.MiniProgram
{
    [DependsOn(typeof(AbpWeChatCommonModule))]
    public class AbpWeChatMiniProgram : AbpModule
    {
    }
}