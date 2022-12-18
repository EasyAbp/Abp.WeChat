using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Services;

public abstract class AbpWeChatServiceBase<TOptions, TApiRequester> : IAbpWeChatService
{
    protected IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    protected TOptions Options { get; }

    public AbpWeChatServiceBase(TOptions options, IAbpLazyServiceProvider lazyServiceProvider)
    {
        Options = options;
        LazyServiceProvider = lazyServiceProvider;
    }

    protected TApiRequester ApiRequester => LazyServiceProvider.LazyGetService<TApiRequester>();
}