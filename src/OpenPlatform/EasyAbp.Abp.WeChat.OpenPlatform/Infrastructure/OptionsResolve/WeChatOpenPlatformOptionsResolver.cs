using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public class WeChatOpenPlatformOptionsResolver : IWeChatOpenPlatformOptionsResolver, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AbpWeChatOpenPlatformResolveOptions _options;

    public WeChatOpenPlatformOptionsResolver(IServiceProvider serviceProvider,
        IOptions<AbpWeChatOpenPlatformResolveOptions> abpWeChatOpenPlatformResolveOptions)
    {
        _serviceProvider = serviceProvider;
        _options = abpWeChatOpenPlatformResolveOptions.Value;
    }

    [Obsolete("Please use asynchronous method.")]
    public IWeChatOpenPlatformOptions Resolve()
    {
        using (var serviceScope = _serviceProvider.CreateScope())
        {
            var context = new WeChatOpenPlatformResolveContext(serviceScope.ServiceProvider);

            foreach (var resolver in _options.WeChatOpenPlatformOptionsResolveContributors)
            {
                resolver.Resolve(context);

                if (context.Options != null)
                {
                    return context.Options;
                }
            }
        }

        return new AbpWeChatOpenPlatformOptions();
    }

    public virtual async ValueTask<IWeChatOpenPlatformOptions> ResolveAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = new WeChatOpenPlatformResolveContext(scope.ServiceProvider);

            foreach (var contributor in _options.WeChatOpenPlatformOptionsResolveContributors)
            {
                await contributor.ResolveAsync(context);

                if (context.Options != null)
                {
                    return context.Options;
                }
            }
        }

        return new AbpWeChatOpenPlatformOptions();
    }
}