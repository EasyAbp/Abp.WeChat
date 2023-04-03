using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

namespace EasyAbp.Abp.WeChat.Official.Options;

public class AbpWeChatOfficialOptions : IAbpWeChatOptions
{
    public string AppId { get; set; }

    public string AppSecret { get; set; }

    public string Token { get; set; }

    public string EncodingAesKey { get; set; }

    public string OAuthRedirectUrl { get; set; }
}