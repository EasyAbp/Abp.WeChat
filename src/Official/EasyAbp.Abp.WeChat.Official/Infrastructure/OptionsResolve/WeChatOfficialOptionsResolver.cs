using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public class WeChatOfficialOptionsResolver : IWeChatOfficialOptionsResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AbpWeChatOfficialResolveOptions _options;

        public WeChatOfficialOptionsResolver(IServiceProvider serviceProvider,
            IOptions<AbpWeChatOfficialResolveOptions> abpWeChatOfficialResolveOptions)
        {
            _serviceProvider = serviceProvider;
            _options = abpWeChatOfficialResolveOptions.Value;
        }

        [Obsolete("Please use asynchronous method.")]
        public IWeChatOfficialOptions Resolve()
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new WeChatOfficialResolveContext(serviceScope.ServiceProvider);

                foreach (var resolver in _options.WeChatOfficialOptionsResolveContributors)
                {
                    resolver.Resolve(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpWeChatOfficialOptions();
        }

        public async ValueTask<IWeChatOfficialOptions> ResolveAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new WeChatOfficialResolveContext(scope.ServiceProvider);

                foreach (var contributor in _options.WeChatOfficialOptionsResolveContributors)
                {
                    await contributor.ResolveAsync(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpWeChatOfficialOptions();
        }
    }
}