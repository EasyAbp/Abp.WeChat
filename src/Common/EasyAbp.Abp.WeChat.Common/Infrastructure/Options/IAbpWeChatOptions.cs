namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

public interface IAbpWeChatOptions
{
    string AppId { get; }

    string AppSecret { get; }

    string Token { get; }

    string EncodingAesKey { get; }
}