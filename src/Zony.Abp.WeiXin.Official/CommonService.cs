using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeiXin.Official.Infrastructure;

namespace Zony.Abp.WeiXin.Official
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

        protected IWeiXinOfficialApiRequester WeiXinOfficialApiRequester => LazyLoadService(ref _weiXinOfficialApiRequester);
        private IWeiXinOfficialApiRequester _weiXinOfficialApiRequester;
    }
}