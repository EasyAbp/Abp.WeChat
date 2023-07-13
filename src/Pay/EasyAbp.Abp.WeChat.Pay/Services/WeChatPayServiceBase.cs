using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using EasyAbp.Abp.WeChat.Pay.Exceptions;
using EasyAbp.Abp.WeChat.Pay.Models;
using EasyAbp.Abp.WeChat.Pay.Options;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services
{
    /// <summary>
    /// 微信支付服务的基类定义，主要提供了常用组件的组入，例如签名生成组件等。
    /// </summary>
    public abstract class WeChatPayServiceBase : IAbpWeChatPayService
    {
        public string MchId => Options.MchId;

        protected IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected AbpWeChatPayOptions Options { get; }

        protected WeChatPayServiceBase(AbpWeChatPayOptions options, IAbpLazyServiceProvider lazyServiceProvider)
        {
            Options = options;
            LazyServiceProvider = lazyServiceProvider;
        }

        protected IWeChatPayApiRequester ApiRequester =>
            LazyServiceProvider.LazyGetRequiredService<IWeChatPayApiRequester>();

        protected ISignatureGenerator SignatureGenerator =>
            LazyServiceProvider.LazyGetRequiredService<ISignatureGenerator>();

        public ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider =>
            LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);

        protected virtual async Task<XmlDocument> RequestAndGetReturnValueAsync(string targetUrl,
            WeChatPayParameters requestParameters, [CanBeNull] string mchId)
        {
            var result = await ApiRequester.RequestAsync(targetUrl, requestParameters.ToXmlStr(), mchId);

            var returnCode = result.SelectSingleNode("/xml/return_code")?.InnerText;
            var returnMsg = result.SelectSingleNode("/xml/return_msg")?.InnerText;

            if (returnCode != "SUCCESS" || (returnMsg != "OK" && !returnMsg.IsNullOrWhiteSpace()))
            {
                var errMsg =
                    $"微信支付接口调用失败，具体失败原因：{result.SelectSingleNode("/xml/err_code_des")?.InnerText ?? result.SelectSingleNode("/xml/return_msg")?.InnerText}";
                Logger.Log(LogLevel.Error, errMsg, targetUrl, requestParameters);

                var exception = new CallWeChatPayApiException(errMsg);
                exception.Data.Add(nameof(targetUrl), targetUrl);
                exception.Data.Add(nameof(requestParameters), requestParameters);

                throw exception;
            }

            return result;
        }

        protected virtual async ValueTask<string> GetRequestUrl(string standardUrl)
        {
            if (Options.IsSandBox)
            {
                throw new NotImplementedException(
                    "原仿真测试系统已不再维护，下线时间2022年5月31日，如有问题请通过新版文档中心技术咨询进行反馈。请需要进行支付测试的商户按照下面新的仿真测试系统说明文档进行接入测试 https://pay.weixin.qq.com/wiki/doc/api/micropay.php?chapter=23_1&index=1");
                return Regex.Replace(standardUrl, "https://api.mch.weixin.qq.com/",
                    "https://api.mch.weixin.qq.com/xdc/apiv2sandbox/");
            }

            return standardUrl;
        }
    }
}