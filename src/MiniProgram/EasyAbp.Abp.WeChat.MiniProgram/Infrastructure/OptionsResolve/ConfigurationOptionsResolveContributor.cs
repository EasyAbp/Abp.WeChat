using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class ConfigurationOptionsResolveContributor : IWeChatMiniProgramOptionsResolveContributor
    {
        public const string ContributorName = "Configuration";
        public string Name => ContributorName;

        public void Resolve(WeChatMiniProgramResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatMiniProgramOptions>>().Value;
        }
    }
}