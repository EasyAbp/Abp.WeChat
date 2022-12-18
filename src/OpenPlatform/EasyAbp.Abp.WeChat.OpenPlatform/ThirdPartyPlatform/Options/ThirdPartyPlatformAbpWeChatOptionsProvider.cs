using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Settings;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

[Dependency(TryRegister = true)]
public class ThirdPartyPlatformAbpWeChatOptionsProvider :
    AbpWeChatOptionsProviderBase<AbpWeChatThirdPartyPlatformOptions>
{
    protected ISettingProvider SettingProvider { get; }

    public ThirdPartyPlatformAbpWeChatOptionsProvider(
        ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }

    public override async Task<AbpWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        var settingAppId = await SettingProvider.GetOrNullAsync(AbpWeChatThirdPartyPlatformSettings.AppId);

        if (settingAppId.IsNullOrWhiteSpace() && appId is null)
        {
            throw new UserFriendlyException("请通过 Settings 或 Options 设置微信应用的 AppId 等相关配置");
        }

        if (appId != null && settingAppId != appId)
        {
            throw new UserFriendlyException(
                "请实现 IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> 以支持多 appid 场景");
        }

        return new AbpWeChatThirdPartyPlatformOptions
        {
            AppId = settingAppId,
            AppSecret = await SettingProvider.GetOrNullAsync(AbpWeChatThirdPartyPlatformSettings.AppSecret),
            Token = await SettingProvider.GetOrNullAsync(AbpWeChatThirdPartyPlatformSettings.Token),
            EncodingAesKey = await SettingProvider.GetOrNullAsync(AbpWeChatThirdPartyPlatformSettings.EncodingAesKey)
        };
    }
}