using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.MiniProgram
{
    public class AbpWeChatMiniProgramOptions : IWeChatMiniProgramOptions
    {
        public string Token { get; set; }
        
        public string OpenAppId { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string EncodingAesKey { get; set; }
    }
}