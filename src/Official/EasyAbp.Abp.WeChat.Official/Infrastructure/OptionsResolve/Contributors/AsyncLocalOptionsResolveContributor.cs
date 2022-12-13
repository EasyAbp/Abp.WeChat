using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors
{
    public class AsyncLocalOptionsResolveContributor : IWeChatOfficialOptionsResolveContributor
    {
        public const string ContributorName = "AsyncLocal";

        public string Name => ContributorName;

        public void Resolve(WeChatOfficialOptionsResolveContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatOfficialAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }
        }

        public ValueTask ResolveAsync(WeChatOfficialOptionsResolveContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatOfficialAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }

            return new ValueTask();
        }
    }

    public interface IWeChatOfficialAsyncLocalAccessor
    {
        IWeChatOfficialOptions Current { get; set; }
    }

    public class WeChatOfficialAsyncLocalAccessor : IWeChatOfficialAsyncLocalAccessor, ISingletonDependency
    {
        public IWeChatOfficialOptions Current
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        private readonly AsyncLocal<IWeChatOfficialOptions> _asyncLocal;

        public WeChatOfficialAsyncLocalAccessor()
        {
            _asyncLocal = new AsyncLocal<IWeChatOfficialOptions>();
        }
    }

    public interface IWeChatOfficialAsyncLocal
    {
        IWeChatOfficialOptions CurrentOptions { get; }

        IDisposable Change(IWeChatOfficialOptions weChatOfficialOptions);
    }

    public class WeChatOfficialAsyncLocal : IWeChatOfficialAsyncLocal, ITransientDependency
    {
        public IWeChatOfficialOptions CurrentOptions { get; private set; }

        private readonly IWeChatOfficialAsyncLocalAccessor _weChatOfficialAsyncLocalAccessor;

        public WeChatOfficialAsyncLocal(IWeChatOfficialAsyncLocalAccessor weChatOfficialAsyncLocalAccessor)
        {
            _weChatOfficialAsyncLocalAccessor = weChatOfficialAsyncLocalAccessor;
            
            CurrentOptions = weChatOfficialAsyncLocalAccessor.Current;
        }

        public IDisposable Change(IWeChatOfficialOptions weChatOfficialOptions)
        {
            var parentScope = _weChatOfficialAsyncLocalAccessor.Current;
            
            _weChatOfficialAsyncLocalAccessor.Current = weChatOfficialOptions;
            
            return new DisposeAction(() =>
            {
                _weChatOfficialAsyncLocalAccessor.Current = parentScope;
            });
        }
    }
}