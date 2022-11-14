using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors
{
    public class AsyncLocalOptionsResolveContributor : IWeChatMiniProgramOptionsResolveContributor
    {
        public const string ContributorName = "AsyncLocal";

        public string Name => ContributorName;

        public virtual Task ResolveAsync(WeChatMiniProgramOptionsResolveContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IWeChatMiniProgramAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }

            return Task.CompletedTask;
        }
    }

    public interface IWeChatMiniProgramAsyncLocalAccessor
    {
        IWeChatMiniProgramOptions Current { get; set; }
    }

    public class WeChatMiniProgramAsyncLocalAccessor : IWeChatMiniProgramAsyncLocalAccessor, ISingletonDependency
    {
        public IWeChatMiniProgramOptions Current
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        private readonly AsyncLocal<IWeChatMiniProgramOptions> _asyncLocal;

        public WeChatMiniProgramAsyncLocalAccessor()
        {
            _asyncLocal = new AsyncLocal<IWeChatMiniProgramOptions>();
        }
    }

    public interface IWeChatMiniProgramAsyncLocal
    {
        IWeChatMiniProgramOptions CurrentOptions { get; }

        IDisposable Change(IWeChatMiniProgramOptions weChatMiniProgramOptions);
    }

    public class WeChatMiniProgramAsyncLocal : IWeChatMiniProgramAsyncLocal, ITransientDependency
    {
        public IWeChatMiniProgramOptions CurrentOptions { get; private set; }

        private readonly IWeChatMiniProgramAsyncLocalAccessor _weChatMiniProgramAsyncLocalAccessor;

        public WeChatMiniProgramAsyncLocal(IWeChatMiniProgramAsyncLocalAccessor weChatMiniProgramAsyncLocalAccessor)
        {
            _weChatMiniProgramAsyncLocalAccessor = weChatMiniProgramAsyncLocalAccessor;
            
            CurrentOptions = weChatMiniProgramAsyncLocalAccessor.Current;
        }

        public IDisposable Change(IWeChatMiniProgramOptions weChatMiniProgramOptions)
        {
            var parentScope = _weChatMiniProgramAsyncLocalAccessor.Current;
            
            _weChatMiniProgramAsyncLocalAccessor.Current = weChatMiniProgramOptions;
            
            return new DisposeAction(() =>
            {
                _weChatMiniProgramAsyncLocalAccessor.Current = parentScope;
            });
        }
    }
}