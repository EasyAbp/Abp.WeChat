using System;
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
    }
}