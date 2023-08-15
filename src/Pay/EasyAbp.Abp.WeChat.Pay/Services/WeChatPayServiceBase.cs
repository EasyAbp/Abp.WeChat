using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services
{
    /// <summary>
    /// 微信支付服务的基类定义，主要提供了常用组件的组入，例如签名生成组件等。
    /// </summary>
    public abstract class WeChatPayServiceBase : IAbpWeChatPayService
    {
        protected IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected AbpWeChatPayOptions Options { get; }

        protected IWeChatPayApiRequester ApiRequester =>
            LazyServiceProvider.LazyGetRequiredService<IWeChatPayApiRequester>();

        public ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

        public string MchId => Options.MchId;

        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider =>
            LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);

        protected WeChatPayServiceBase(AbpWeChatPayOptions options, IAbpLazyServiceProvider lazyServiceProvider)
        {
            Options = options;
            LazyServiceProvider = lazyServiceProvider;
        }
    }
}