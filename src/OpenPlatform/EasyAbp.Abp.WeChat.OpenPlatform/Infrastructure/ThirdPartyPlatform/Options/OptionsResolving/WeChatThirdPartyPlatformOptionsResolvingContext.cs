using System;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;

public class WeChatThirdPartyPlatformOptionsResolvingContext : IServiceProviderAccessor
{
    public IWeChatThirdPartyPlatformOptions Options { get; set; }

    public IServiceProvider ServiceProvider { get; }

    public WeChatThirdPartyPlatformOptionsResolvingContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}