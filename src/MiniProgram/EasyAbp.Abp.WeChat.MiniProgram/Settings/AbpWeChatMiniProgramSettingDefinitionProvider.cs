using EasyAbp.Abp.WeChat.MiniProgram.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.MiniProgram.Settings;

public class AbpWeChatMiniProgramSettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatMiniProgramOptions _options;

    public AbpWeChatMiniProgramSettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatMiniProgramOptions> options)
    {
        _stringEncryptionService = stringEncryptionService;
        _options = options.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppId,
            _options.AppId
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppSecret,
            _stringEncryptionService.Encrypt(_options.AppSecret),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.Token,
            _stringEncryptionService.Encrypt(_options.Token),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.EncodingAesKey,
            _stringEncryptionService.Encrypt(_options.EncodingAesKey),
            isEncrypted: true
        ));
    }
}