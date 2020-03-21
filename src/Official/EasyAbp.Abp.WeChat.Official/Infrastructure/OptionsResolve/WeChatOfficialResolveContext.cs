using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public class WeChatOfficialResolveContext : IServiceProviderAccessor
    {
        public IWeChatOfficialOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public WeChatOfficialResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}