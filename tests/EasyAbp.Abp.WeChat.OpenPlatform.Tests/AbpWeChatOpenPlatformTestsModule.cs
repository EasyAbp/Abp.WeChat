using Volo.Abp.Modularity;
using EasyAbp.Abp.WeChat.Common.Tests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests;

[DependsOn(typeof(AbpWeChatCommonTestsModule),
    typeof(AbpWeChatOpenPlatformModule))]
public class AbpWeChatOpenPlatformTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatThirdPartyPlatformOptions>(op =>
        {
            op.AppId = AbpWeChatOpenPlatformTestsConsts.AppId;
            op.AppSecret = AbpWeChatOpenPlatformTestsConsts.AppSecret;
            op.Token = AbpWeChatOpenPlatformTestsConsts.Token;
            op.EncodingAesKey = AbpWeChatOpenPlatformTestsConsts.EncodingAesKey;
        });
    }
}