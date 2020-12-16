using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
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