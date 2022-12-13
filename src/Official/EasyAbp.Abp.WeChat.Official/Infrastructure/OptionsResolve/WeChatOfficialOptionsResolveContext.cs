using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public class WeChatOfficialOptionsResolveContext : IServiceProviderAccessor
    {
        public IWeChatOfficialOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public WeChatOfficialOptionsResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}