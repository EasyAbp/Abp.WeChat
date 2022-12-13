using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors
{
    public class ConfigurationOptionsResolveContributor : IWeChatOfficialOptionsResolveContributor
    {
        public const string ContributorName = "Configuration";
        public string Name => ContributorName;

        public void Resolve(WeChatOfficialOptionsResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatOfficialOptions>>().Value;
        }

        public ValueTask ResolveAsync(WeChatOfficialOptionsResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatOfficialOptions>>().Value;

            return new ValueTask();
        }
    }
}