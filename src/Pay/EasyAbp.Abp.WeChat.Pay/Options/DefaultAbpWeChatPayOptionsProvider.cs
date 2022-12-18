using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Settings;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Pay.Options;

[Dependency(TryRegister = true)]
public class DefaultAbpWeChatPayOptionsProvider : IAbpWeChatPayOptionsProvider, ITransientDependency
{
    protected ISettingProvider SettingProvider { get; }

    public DefaultAbpWeChatPayOptionsProvider(ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }

    public virtual async Task<AbpWeChatPayOptions> GetAsync(string mchId)
    {
        var settingMchId = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.MchId);

        if (settingMchId.IsNullOrWhiteSpace() && mchId is null)
        {
            throw new UserFriendlyException("请通过 Settings 或 Options 设置微信应用的 MchId 等相关配置");
        }

        if (mchId != null && settingMchId != mchId)
        {
            throw new UserFriendlyException("请实现 IAbpWeChatPayOptionsProvider 以支持多商户场景");
        }

        return new AbpWeChatPayOptions
        {
            MchId = settingMchId,
            ApiKey = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.ApiKey),
            IsSandBox = await SettingProvider.GetAsync<bool>(AbpWeChatPaySettings.IsSandBox),
            NotifyUrl = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.NotifyUrl),
            RefundNotifyUrl = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.RefundNotifyUrl),
            CertificateBlobContainerName =
                await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.CertificateBlobContainerName),
            CertificateBlobName = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.CertificateBlobName),
            CertificateSecret = await SettingProvider.GetOrNullAsync(AbpWeChatPaySettings.CertificateSecret)
        };
    }
}