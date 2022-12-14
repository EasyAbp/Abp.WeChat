using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;

public class WeChatThirdPartyPlatformOptionsResolver : IWeChatThirdPartyPlatformOptionsResolver, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AbpWeChatThirdPartyPlatformResolvingOptions _options;

    public WeChatThirdPartyPlatformOptionsResolver(IServiceProvider serviceProvider,
        IOptions<AbpWeChatThirdPartyPlatformResolvingOptions> abpWeChatOpenPlatformResolveOptions)
    {
        _serviceProvider = serviceProvider;
        _options = abpWeChatOpenPlatformResolveOptions.Value;
    }

    [Obsolete("Please use asynchronous method.")]
    public IWeChatThirdPartyPlatformOptions Resolve()
    {
        using (var serviceScope = _serviceProvider.CreateScope())
        {
            var context = new WeChatThirdPartyPlatformOptionsResolvingContext(serviceScope.ServiceProvider);

            foreach (var resolver in _options.WeChatThirdPartyPlatformOptionsResolveContributors)
            {
                resolver.Resolve(context);

                if (context.Options != null)
                {
                    return context.Options;
                }
            }
        }

        return new AbpWeChatThirdPartyPlatformOptions();
    }

    public virtual async ValueTask<IWeChatThirdPartyPlatformOptions> ResolveAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = new WeChatThirdPartyPlatformOptionsResolvingContext(scope.ServiceProvider);

            foreach (var contributor in _options.WeChatThirdPartyPlatformOptionsResolveContributors)
            {
                await contributor.ResolveAsync(context);

                if (context.Options != null)
                {
                    return context.Options;
                }
            }
        }

        return new AbpWeChatThirdPartyPlatformOptions();
    }
}