using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform;

public class AbpWeChatThirdPartyPlatformOptions : IWeChatThirdPartyPlatformOptions
{
    public string Token { get; set; }

    public string AppId { get; set; }

    public string AppSecret { get; set; }

    public string EncodingAesKey { get; set; }
}