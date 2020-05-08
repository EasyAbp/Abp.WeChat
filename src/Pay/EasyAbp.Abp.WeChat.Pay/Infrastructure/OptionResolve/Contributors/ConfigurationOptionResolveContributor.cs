using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
    public class ConfigurationOptionResolveContributor : IWeChatPayOptionResolveContributor
    {
        public const string ContributorName = "Configuration";

        public string Name => ContributorName;

        public Task ResolveAsync(WeChatPayOptionsResolverContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;
            return Task.CompletedTask;
        }
    }
}