using System;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services;

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

    protected IWeChatOpenPlatformApiRequester WeChatOpenPlatformApiRequester =>
        LazyLoadService(ref _weChatOpenPlatformApiRequester);

    private IWeChatOpenPlatformApiRequester _weChatOpenPlatformApiRequester;
}