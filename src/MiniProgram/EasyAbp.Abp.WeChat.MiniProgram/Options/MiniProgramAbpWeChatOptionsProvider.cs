using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Settings;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.MiniProgram.Options;

[Dependency(TryRegister = true)]
public class MiniProgramAbpWeChatOptionsProvider : AbpWeChatOptionsProviderBase<AbpWeChatMiniProgramOptions>
{
    protected ISettingProvider SettingProvider { get; }

    public MiniProgramAbpWeChatOptionsProvider(
        ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }

    public override async Task<AbpWeChatMiniProgramOptions> GetAsync(string appId)
    {
        var settingAppId = await SettingProvider.GetOrNullAsync(AbpWeChatMiniProgramSettings.AppId);

        if (settingAppId.IsNullOrWhiteSpace() && appId is null)
        {
            throw new UserFriendlyException("请通过 Settings 或 Options 设置微信应用的 AppId 等相关配置");
        }

        if (appId != null && settingAppId != appId)
        {
            throw new UserFriendlyException("请实现 IAbpWeChatOptionsProvider<AbpWeChatMiniProgramOptions> 以支持多 appid 场景");
        }

        return new AbpWeChatMiniProgramOptions
        {
            AppId = settingAppId,
            AppSecret = await SettingProvider.GetOrNullAsync(AbpWeChatMiniProgramSettings.AppSecret),
            Token = await SettingProvider.GetOrNullAsync(AbpWeChatMiniProgramSettings.Token),
            EncodingAesKey = await SettingProvider.GetOrNullAsync(AbpWeChatMiniProgramSettings.EncodingAesKey)
        };
    }
}