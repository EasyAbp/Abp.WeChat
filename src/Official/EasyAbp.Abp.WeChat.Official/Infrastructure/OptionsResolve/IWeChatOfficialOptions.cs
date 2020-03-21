namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptions
    {
        /// <summary>
        /// 消息加密的 Token。
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// 微信公众号的 AppId。
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        /// 微信公众号的 API Secret。
        /// </summary>
        string AppSecret { get; set; }

        string EncodingAesKey { get; set; }

        /// <summary>
        /// 微信网页授权成功后，重定向的 URL。
        /// </summary>
        string OAuthRedirectUrl { get; set; }
    }
}