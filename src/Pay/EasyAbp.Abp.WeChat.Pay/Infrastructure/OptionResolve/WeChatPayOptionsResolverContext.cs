using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public class WeChatPayOptionsResolverContext : IServiceProviderAccessor
    {
        public IWeChatPayOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public WeChatPayOptionsResolverContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}