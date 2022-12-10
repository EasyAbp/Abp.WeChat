using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(typeof(AbpWeChatOpenPlatformModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AbpWeChatOpenPlatformHttpApiModule : AbpModule
{
}