using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class WeChatMiniProgramOptionsResolver : IWeChatMiniProgramOptionsResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AbpWeChatMiniProgramResolveOptions _options;

        public WeChatMiniProgramOptionsResolver(IServiceProvider serviceProvider,
            IOptions<AbpWeChatMiniProgramResolveOptions> abpWeChatMiniProgramResolveOptions)
        {
            _serviceProvider = serviceProvider;
            _options = abpWeChatMiniProgramResolveOptions.Value;
        }

        public async Task<IWeChatMiniProgramOptions> ResolveAsync()
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new WeChatMiniProgramOptionsResolveContext(serviceScope.ServiceProvider);

                foreach (var resolver in _options.Contributors)
                {
                    await resolver.ResolveAsync(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpWeChatMiniProgramOptions();
        }
    }
}