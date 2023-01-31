using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

namespace EasyAbp.Abp.WeChat.Official.Options;

public class AbpWeChatOfficialOptions : IAbpWeChatOptions
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

    public string OAuthRedirectUrl { get; set; }
}