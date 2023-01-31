using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

public class AbpWeChatThirdPartyPlatformOptions : IAbpWeChatOptions
{
    public string AppId { get; set; }

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public string AppSecret { get; set; }

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public string EncodingAesKey { get; set; }
}