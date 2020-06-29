using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Services
{
    public abstract class CommonService : ITransientDependency
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

        protected IAccessTokenAccessor AccessTokenAccessor => LazyLoadService(ref _accessTokenAccessor);
        private IAccessTokenAccessor _accessTokenAccessor;

        protected IWeChatMiniProgramApiRequester WeChatMiniProgramApiRequester => LazyLoadService(ref _weChatMiniProgramApiRequester);
        private IWeChatMiniProgramApiRequester _weChatMiniProgramApiRequester;
    }
}