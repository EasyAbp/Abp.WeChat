using EasyAbp.Abp.WeChat.Common;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(
    typeof(AbpWeChatCommonModule),
    typeof(AbpWeChatOpenPlatformAbstractionsModule)
)]
public class AbpWeChatOpenPlatformModule : AbpModule 
{
}