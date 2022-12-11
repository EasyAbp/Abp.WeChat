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
        /// 微信小程序的 AppId。
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        /// 微信小程序的 API Secret。
        /// 如果安装了微信第三方平台模块，此值留空时，接口调用的 access_token 将由微信第三方平台接管。
        /// </summary>
        string AppSecret { get; set; }

        string EncodingAesKey { get; set; }
    }
}