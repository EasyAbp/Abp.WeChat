using System.Threading;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
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
}