using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
    public class AsyncLocalOptionsResolveContributor : IWeChatPayOptionsResolveContributor
    {
        public const string ContributorName = "AsyncLocal";

        public string Name => ContributorName;

        public virtual Task ResolveAsync(WeChatPayOptionsResolverContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatPayAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }

            return Task.CompletedTask;
        }
    }
}