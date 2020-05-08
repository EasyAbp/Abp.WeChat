using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public interface IWeChatPayOptionsResolver
    {
        Task<IWeChatPayOptions> ResolveAsync();
    }

    public class WeChatPayOptionsResolver : IWeChatPayOptionsResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AbpWeChatPayResolveOptions _options;

        public WeChatPayOptionsResolver(IServiceProvider serviceProvider, IOptions<AbpWeChatPayResolveOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public async Task<IWeChatPayOptions> ResolveAsync()
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new WeChatPayOptionsResolverContext(serviceScope.ServiceProvider);

                foreach (var resolver in _options.ResolveContributors)
                {
                    await resolver.ResolveAsync(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpWeChatPayOptions();
        }
    }
}