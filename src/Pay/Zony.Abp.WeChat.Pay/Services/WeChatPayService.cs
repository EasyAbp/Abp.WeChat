using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeChat.Pay.Infrastructure;

namespace Zony.Abp.WeChat.Pay.Services
{
    public abstract class WeChatPayService : ITransientDependency
    {
        public IServiceProvider ServiceProvider { get; set; }
        
        protected readonly object ServiceLocker = new object();
        protected TService LazyLoadService<TService>(ref TService service)
        {
            if (service == null)
            {
                lock (ServiceLocker)
                {
                    if (service == null)
                    {
                        service = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return service;
        }

        protected ISignatureGenerator SignatureGenerator => LazyLoadService(ref _signatureGenerator);
        private ISignatureGenerator _signatureGenerator;

        protected IWeChatPayApiRequester WeChatPayApiRequester => LazyLoadService(ref _weChatPayApiRequester);
        private IWeChatPayApiRequester _weChatPayApiRequester;
    }
}