using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

public abstract class AbpWeChatOptionsProviderBase<TOptions> : IAbpWeChatOptionsProvider<TOptions>, ITransientDependency
    where TOptions : IAbpWeChatOptions
{
    public abstract Task<TOptions> GetAsync(string appId);

    public async Task<IAbpWeChatOptions> GetBasicAsync(string appId)
    {
        return await GetAsync(appId);
    }
}