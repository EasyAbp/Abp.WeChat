using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public class WeChatOpenPlatformResolveContext : IServiceProviderAccessor
{
    public IWeChatOpenPlatformOptions Options { get; set; }

    public IServiceProvider ServiceProvider { get; }

    public WeChatOpenPlatformResolveContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}