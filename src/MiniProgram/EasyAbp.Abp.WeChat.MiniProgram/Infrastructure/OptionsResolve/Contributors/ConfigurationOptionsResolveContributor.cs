using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors
{
    public class ConfigurationOptionsResolveContributor : IWeChatMiniProgramOptionsResolveContributor
    {
        public const string ContributorName = "Configuration";
        public string Name => ContributorName;

        public virtual Task ResolveAsync(WeChatMiniProgramOptionsResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpWeChatMiniProgramOptions>>().Value;
            
            return Task.CompletedTask;
        }
    }
}