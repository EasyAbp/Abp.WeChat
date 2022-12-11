using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;

public class WeChatOpenPlatformResolveContext : IServiceProviderAccessor
{
    public IWeChatThirdPartyPlatformOptions Options { get; set; }

    public IServiceProvider ServiceProvider { get; }

    public WeChatOpenPlatformResolveContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}