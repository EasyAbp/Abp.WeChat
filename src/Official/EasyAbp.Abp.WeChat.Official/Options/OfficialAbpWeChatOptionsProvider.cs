using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official.Settings;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Official.Options;

[Dependency(TryRegister = true)]
public class OfficialAbpWeChatOptionsProvider : AbpWeChatOptionsProviderBase<AbpWeChatOfficialOptions>
{
    protected ISettingProvider SettingProvider { get; }

    public OfficialAbpWeChatOptionsProvider(
        ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }

    public override async Task<AbpWeChatOfficialOptions> GetAsync(string appId)
    {
        var settingAppId = await SettingProvider.GetOrNullAsync(AbpWeChatOfficialSettings.AppId);

        if (settingAppId.IsNullOrWhiteSpace() && appId is null)
        {
            throw new UserFriendlyException("请通过 Settings 或 Options 设置微信应用的 AppId 等相关配置");
        }

        if (appId != null && settingAppId != appId)
        {
            throw new UserFriendlyException("请实现 IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> 以支持多 appid 场景");
        }

        return new AbpWeChatOfficialOptions
        {
            AppId = settingAppId,
            AppSecret = await SettingProvider.GetOrNullAsync(AbpWeChatOfficialSettings.AppSecret),
            Token = await SettingProvider.GetOrNullAsync(AbpWeChatOfficialSettings.Token),
            EncodingAesKey = await SettingProvider.GetOrNullAsync(AbpWeChatOfficialSettings.EncodingAesKey),
            OAuthRedirectUrl = await SettingProvider.GetOrNullAsync(AbpWeChatOfficialSettings.OAuthRedirectUrl)
        };
    }
}