using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

namespace EasyAbp.Abp.WeChat.MiniProgram.Options
{
    public class AbpWeChatMiniProgramOptions : IAbpWeChatOptions
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAesKey { get; set; }
    }
}