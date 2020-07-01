using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class WeChatMiniProgramResolveContext : IServiceProviderAccessor
    {
        public IWeChatMiniProgramOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public WeChatMiniProgramResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}