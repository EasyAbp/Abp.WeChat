using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public class ConfigurationOptionsResolveContributor : IWeChatOfficialOptionsResolveContributor
    {
        public const string ContributorName = "Configuration";
        public string Name => ContributorName;

        public void Resolve(WeChatOfficialResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatOfficialOptions>>().Value;
        }
    }
}