using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform;

namespace EasyAbp.Abp.WeChat.Official
{
    [DependsOn(
        typeof(AbpWeChatCommonModule),
        typeof(AbpWeChatOfficialAbstractionsModule)
    )]
    public class AbpWeChatOfficialModule : AbpModule
    {
    }
}