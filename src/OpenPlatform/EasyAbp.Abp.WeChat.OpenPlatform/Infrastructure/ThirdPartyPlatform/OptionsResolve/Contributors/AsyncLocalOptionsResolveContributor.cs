using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve.Contributors;

public class AsyncLocalOptionsResolveContributor : IWeChatThirdPartyPlatformOptionsResolveContributor
{
    public const string ContributorName = "AsyncLocal";

    public string Name => ContributorName;

    public void Resolve(WeChatThirdPartyPlatformOptionsResolveContext context)
    {
        var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatThirdPartyPlatformAsyncLocalAccessor>();

        if (asyncLocal.Current != null)
        {
            context.Options = asyncLocal.Current;
        }
    }

    public ValueTask ResolveAsync(WeChatThirdPartyPlatformOptionsResolveContext context)
    {
        var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatThirdPartyPlatformAsyncLocalAccessor>();

        if (asyncLocal.Current != null)
        {
            context.Options = asyncLocal.Current;
        }

        return new ValueTask();
    }
}

public interface IWeChatThirdPartyPlatformAsyncLocalAccessor
{
    IWeChatThirdPartyPlatformOptions Current { get; set; }
}

public class WeChatThirdPartyPlatformAsyncLocalAccessor : IWeChatThirdPartyPlatformAsyncLocalAccessor, ISingletonDependency
{
    public IWeChatThirdPartyPlatformOptions Current
    {
        get => _asyncLocal.Value;
        set => _asyncLocal.Value = value;
    }

    private readonly AsyncLocal<IWeChatThirdPartyPlatformOptions> _asyncLocal;

    public WeChatThirdPartyPlatformAsyncLocalAccessor()
    {
        _asyncLocal = new AsyncLocal<IWeChatThirdPartyPlatformOptions>();
    }
}

public interface IWeChatThirdPartyPlatformAsyncLocal
{
    IWeChatThirdPartyPlatformOptions CurrentOptions { get; }

    IDisposable Change(IWeChatThirdPartyPlatformOptions weChatThirdPartyPlatformOptions);
}

public class WeChatThirdPartyPlatformAsyncLocal : IWeChatThirdPartyPlatformAsyncLocal, ITransientDependency
{
    public IWeChatThirdPartyPlatformOptions CurrentOptions { get; private set; }

    private readonly IWeChatThirdPartyPlatformAsyncLocalAccessor _weChatThirdPartyPlatformAsyncLocalAccessor;

    public WeChatThirdPartyPlatformAsyncLocal(IWeChatThirdPartyPlatformAsyncLocalAccessor weChatThirdPartyPlatformAsyncLocalAccessor)
    {
        _weChatThirdPartyPlatformAsyncLocalAccessor = weChatThirdPartyPlatformAsyncLocalAccessor;
            
        CurrentOptions = weChatThirdPartyPlatformAsyncLocalAccessor.Current;
    }

    public IDisposable Change(IWeChatThirdPartyPlatformOptions weChatThirdPartyPlatformOptions)
    {
        var parentScope = _weChatThirdPartyPlatformAsyncLocalAccessor.Current;
            
        _weChatThirdPartyPlatformAsyncLocalAccessor.Current = weChatThirdPartyPlatformOptions;
            
        return new DisposeAction(() =>
        {
            _weChatThirdPartyPlatformAsyncLocalAccessor.Current = parentScope;
        });
    }
}