using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

[DependsOn(
    typeof(AbpWeChatCommonModule),
    typeof(AbpWeChatOpenPlatformAbstractionsModule)
)]
public class AbpWeChatOpenPlatformModule : AbpModule 
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICurrentWeChatThirdPartyPlatformAccessor>(
            CurrentWeChatThirdPartyPlatformAccessor.Instance);
    }
}