using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(typeof(AbpWeChatOpenPlatformAbstractionsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpWeChatOpenPlatformHttpApiModule : AbpModule
{
}