using EasyAbp.Abp.WeChat.Pay.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Pay.Settings;

public class AbpWeChatPaySettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatPayOptions _options;

    public AbpWeChatPaySettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatPayOptions> options)
    {
        _stringEncryptionService = stringEncryptionService;
        _options = options.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.MchId,
            _options.MchId
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.ApiKey,
            _stringEncryptionService.Encrypt(_options.ApiKey),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.IsSandBox,
            _options.IsSandBox.ToString()
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.NotifyUrl,
            _options.NotifyUrl
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.RefundNotifyUrl,
            _options.RefundNotifyUrl
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateBlobContainerName,
            _options.CertificateBlobContainerName
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateBlobName,
            _options.CertificateBlobName
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateSecret,
            _options.CertificateSecret,
            isEncrypted: true
        ));
    }
}