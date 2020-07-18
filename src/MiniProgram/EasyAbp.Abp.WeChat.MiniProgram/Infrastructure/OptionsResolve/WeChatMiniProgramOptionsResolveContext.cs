using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class WeChatMiniProgramOptionsResolveContext : IServiceProviderAccessor
    {
        public IWeChatMiniProgramOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public WeChatMiniProgramOptionsResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}