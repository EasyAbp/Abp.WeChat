using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

namespace EasyAbp.Abp.WeChat.Common.Tests.Encryptors;

public class TestAbpWeChatOptions : IAbpWeChatOptions
{
    public string AppId { get; set; }

    public string AppSecret { get; set; }

    public string Token { get; set; }

    public string EncodingAesKey { get; set; }
}