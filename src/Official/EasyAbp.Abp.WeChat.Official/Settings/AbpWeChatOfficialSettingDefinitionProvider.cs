using EasyAbp.Abp.WeChat.Official.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Official.Settings;

public class AbpWeChatOfficialSettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatOfficialOptions _options;

    public AbpWeChatOfficialSettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatOfficialOptions> options)
    {
        _stringEncryptionService = stringEncryptionService;
        _options = options.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(
            AbpWeChatOfficialSettings.AppId,
            _options.AppId
        ));

        context.Add(new SettingDefinition(
            AbpWeChatOfficialSettings.AppSecret,
            _stringEncryptionService.Encrypt(_options.AppSecret),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatOfficialSettings.Token,
            _stringEncryptionService.Encrypt(_options.Token),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatOfficialSettings.EncodingAesKey,
            _stringEncryptionService.Encrypt(_options.EncodingAesKey),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatOfficialSettings.OAuthRedirectUrl,
            _options.OAuthRedirectUrl
        ));
    }
}