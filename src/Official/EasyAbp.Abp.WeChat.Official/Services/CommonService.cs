using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using EasyAbp.Abp.WeChat.Official.Infrastructure;

namespace EasyAbp.Abp.WeChat.Official.Services
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

        protected IWeChatOfficialApiRequester WeChatOfficialApiRequester => LazyLoadService(ref _weChatOfficialApiRequester);
        private IWeChatOfficialApiRequester _weChatOfficialApiRequester;
    }
}