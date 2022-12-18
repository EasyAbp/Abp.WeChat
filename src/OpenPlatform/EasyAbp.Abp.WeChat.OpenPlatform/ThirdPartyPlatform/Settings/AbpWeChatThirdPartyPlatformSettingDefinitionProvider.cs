using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Settings;

public class AbpWeChatThirdPartyPlatformSettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatThirdPartyPlatformOptions _options;

    public AbpWeChatThirdPartyPlatformSettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatThirdPartyPlatformOptions> options)
    {
        _stringEncryptionService = stringEncryptionService;
        _options = options.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(
            AbpWeChatThirdPartyPlatformSettings.AppId,
            _options.AppId
        ));

        context.Add(new SettingDefinition(
            AbpWeChatThirdPartyPlatformSettings.AppSecret,
            _stringEncryptionService.Encrypt(_options.AppSecret),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatThirdPartyPlatformSettings.Token,
            _stringEncryptionService.Encrypt(_options.Token),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatThirdPartyPlatformSettings.EncodingAesKey,
            _stringEncryptionService.Encrypt(_options.EncodingAesKey),
            isEncrypted: true
        ));
    }
}