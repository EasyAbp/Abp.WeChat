using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve.Contributors;

public class AsyncLocalOptionsResolveContributor : IWeChatOpenPlatformOptionsResolveContributor
{
    public const string ContributorName = "AsyncLocal";

    public string Name => ContributorName;

    public void Resolve(WeChatOpenPlatformResolveContext context)
    {
        var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatOpenPlatformAsyncLocalAccessor>();

        if (asyncLocal.Current != null)
        {
            context.Options = asyncLocal.Current;
        }
    }

    public ValueTask ResolveAsync(WeChatOpenPlatformResolveContext context)
    {
        var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatOpenPlatformAsyncLocalAccessor>();

        if (asyncLocal.Current != null)
        {
            context.Options = asyncLocal.Current;
        }

        return new ValueTask();
    }
}

public interface IWeChatOpenPlatformAsyncLocalAccessor
{
    IWeChatOpenPlatformOptions Current { get; set; }
}

public class WeChatOpenPlatformAsyncLocalAccessor : IWeChatOpenPlatformAsyncLocalAccessor, ISingletonDependency
{
    public IWeChatOpenPlatformOptions Current
    {
        get => _asyncLocal.Value;
        set => _asyncLocal.Value = value;
    }

    private readonly AsyncLocal<IWeChatOpenPlatformOptions> _asyncLocal;

    public WeChatOpenPlatformAsyncLocalAccessor()
    {
        _asyncLocal = new AsyncLocal<IWeChatOpenPlatformOptions>();
    }
}

public interface IWeChatOpenPlatformAsyncLocal
{
    IWeChatOpenPlatformOptions CurrentOptions { get; }

    IDisposable Change(IWeChatOpenPlatformOptions weChatOpenPlatformOptions);
}

public class WeChatOpenPlatformAsyncLocal : IWeChatOpenPlatformAsyncLocal, ITransientDependency
{
    public IWeChatOpenPlatformOptions CurrentOptions { get; private set; }

    private readonly IWeChatOpenPlatformAsyncLocalAccessor _weChatOpenPlatformAsyncLocalAccessor;

    public WeChatOpenPlatformAsyncLocal(IWeChatOpenPlatformAsyncLocalAccessor weChatOpenPlatformAsyncLocalAccessor)
    {
        _weChatOpenPlatformAsyncLocalAccessor = weChatOpenPlatformAsyncLocalAccessor;
            
        CurrentOptions = weChatOpenPlatformAsyncLocalAccessor.Current;
    }

    public IDisposable Change(IWeChatOpenPlatformOptions weChatOpenPlatformOptions)
    {
        var parentScope = _weChatOpenPlatformAsyncLocalAccessor.Current;
            
        _weChatOpenPlatformAsyncLocalAccessor.Current = weChatOpenPlatformOptions;
            
        return new DisposeAction(() =>
        {
            _weChatOpenPlatformAsyncLocalAccessor.Current = parentScope;
        });
    }
}