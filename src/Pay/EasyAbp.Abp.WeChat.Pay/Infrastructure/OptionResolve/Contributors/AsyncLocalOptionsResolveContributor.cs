using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
    public class AsyncLocalOptionsResolveContributor : IWeChatPayOptionsResolveContributor
    {
        public const string ContributorName = "AsyncLocal";

        public string Name => ContributorName;

        public Task ResolveAsync(WeChatPayOptionsResolverContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatPayAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }

            return Task.CompletedTask;
        }
    }

    public interface IWeChatPayAsyncLocalAccessor
    {
        IWeChatPayOptions Current { get; set; }
    }

    public class WeChatPayAsyncLocalAccessor : IWeChatPayAsyncLocalAccessor, ISingletonDependency
    {
        public IWeChatPayOptions Current
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        private readonly AsyncLocal<IWeChatPayOptions> _asyncLocal;

        public WeChatPayAsyncLocalAccessor()
        {
            _asyncLocal = new AsyncLocal<IWeChatPayOptions>();
        }
    }

    public interface IWeChatPayAsyncLocal
    {
        IWeChatPayOptions CurrentOptions { get; }

        IDisposable Change(IWeChatPayOptions weChatPayOptions);
    }

    public class WeChatPayAsyncLocal : IWeChatPayAsyncLocal, ITransientDependency
    {
        public IWeChatPayOptions CurrentOptions { get; }

        private readonly IWeChatPayAsyncLocalAccessor _weChatPayAsyncLocalAccessor;

        public WeChatPayAsyncLocal(IWeChatPayAsyncLocalAccessor weChatPayAsyncLocalAccessor)
        {
            _weChatPayAsyncLocalAccessor = weChatPayAsyncLocalAccessor;
            
            CurrentOptions = weChatPayAsyncLocalAccessor.Current;
        }

        public IDisposable Change(IWeChatPayOptions weChatPayOptions)
        {
            var parentScope = _weChatPayAsyncLocalAccessor.Current;
            
            _weChatPayAsyncLocalAccessor.Current = weChatPayOptions;
            
            return new DisposeAction(() =>
            {
                _weChatPayAsyncLocalAccessor.Current = parentScope;
            });
        }
    }
}