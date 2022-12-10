using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

public class AbpWeChatOpenPlatformOptions : IWeChatOpenPlatformOptions
{
    public string Token { get; set; }

    public string AppId { get; set; }

    public string AppSecret { get; set; }

    public string EncodingAesKey { get; set; }
}