namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public interface IWeChatMiniProgramOptions
    {
        /// <summary>
        /// 消息加密的 Token。
        /// </summary>
        string Token { get; set; }
        
        string OpenAppId { get; set; }

        /// <summary>
        /// 微信公众号的 AppId。
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        /// 微信公众号的 API Secret。
        /// </summary>
        string AppSecret { get; set; }

        string EncodingAesKey { get; set; }
    }
}