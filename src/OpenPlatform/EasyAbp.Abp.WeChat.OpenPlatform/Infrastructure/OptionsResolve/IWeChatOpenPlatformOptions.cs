namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public interface IWeChatOpenPlatformOptions
{
    /// <summary>
    /// 消息加密的 Token。
    /// </summary>
    string Token { get; set; }

    /// <summary>
    /// 微信第三方平台的 AppId。
    /// </summary>
    string AppId { get; set; }

    /// <summary>
    /// 微信第三方平台的 API Secret。
    /// </summary>
    string AppSecret { get; set; }

    string EncodingAesKey { get; set; }
}