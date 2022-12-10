using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests;

[DependsOn(typeof(AbpWeChatCommonTestsModule),
    typeof(AbpWeChatOpenPlatformModule))]
public class AbpWeChatOpenPlatformTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatOpenPlatformOptions>(op =>
        {
            op.AppId = AbpWeChatOpenPlatformTestsConsts.AppId;
            op.Token = AbpWeChatOpenPlatformTestsConsts.Token;
            op.EncodingAesKey = AbpWeChatOpenPlatformTestsConsts.EncodingAESKey;
        });
    }
}