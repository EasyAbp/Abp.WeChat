using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;

public class WeChatThirdPartyPlatformOptionsResolveContext : IServiceProviderAccessor
{
    public IWeChatThirdPartyPlatformOptions Options { get; set; }

    public IServiceProvider ServiceProvider { get; }

    public WeChatThirdPartyPlatformOptionsResolveContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}