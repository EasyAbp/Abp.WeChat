using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventRequestHandlingService : IWeChatPayEventRequestHandlingService, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    private readonly IWeChatPayEventXmlDecrypter _xmlDecrypter;
    private readonly IAbpWeChatPayOptionsProvider _optionsProvider;
    private readonly ISignatureGenerator _signatureGenerator;

    public WeChatPayEventRequestHandlingService(
        IWeChatPayEventXmlDecrypter xmlDecrypter,
        IAbpWeChatPayOptionsProvider optionsProvider,
        ISignatureGenerator signatureGenerator)
    {
        _xmlDecrypter = xmlDecrypter;
        _optionsProvider = optionsProvider;
        _signatureGenerator = signatureGenerator;
    }

    public virtual async Task<WeChatRequestHandlingResult> NotifyAsync(string mchId, XmlDocument xmlDocument)
    {
        var options = await _optionsProvider.GetAsync(mchId);

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayEventHandler>>()
            .Where(h => h.Type == WeChatHandlerType.Paid);

        if (!await IsSignValidAsync(xmlDocument, options))
        {
            return new WeChatRequestHandlingResult(false, "签名验证不通过");
        }

        var model = new WeChatPayEventModel
        {
            Options = options,
            WeChatRequestXmlData = xmlDocument
        };

        foreach (var handler in handlers.Where(x => x.Type == WeChatHandlerType.Paid))
        {
            var result = await handler.HandleAsync(model);

            if (!result.Success)
            {
                return result;
            }
        }

        return new WeChatRequestHandlingResult(true);
    }

    public virtual async Task<WeChatRequestHandlingResult> RefundNotifyAsync(string mchId, XmlDocument xmlDocument)
    {
        var options = await _optionsProvider.GetAsync(mchId);

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayEventHandler>>()
            .Where(x => x.Type == WeChatHandlerType.Refund);

        var (decryptingResult, decryptedXmlDocument) = await _xmlDecrypter.TryDecryptAsync(xmlDocument, options);

        if (!decryptingResult)
        {
            return new WeChatRequestHandlingResult(false, "微信消息体解码失败");
        }

        var model = new WeChatPayEventModel
        {
            Options = options,
            WeChatRequestXmlData = xmlDocument,
            DecryptedXmlData = decryptedXmlDocument
        };

        foreach (var handler in handlers)
        {
            var result = await handler.HandleAsync(model);

            if (!result.Success)
            {
                return result;
            }
        }

        return new WeChatRequestHandlingResult(true);
    }

    protected virtual Task<bool> IsSignValidAsync(XmlDocument weChatRequestXmlData, AbpWeChatPayOptions options)
    {
        var parameters = new WeChatParameters();

        var nodes = weChatRequestXmlData.SelectSingleNode("/xml")?.ChildNodes;
        if (nodes == null)
        {
            return Task.FromResult(false);
        }

        foreach (XmlNode node in nodes)
        {
            if (node.Name == "sign")
            {
                continue;
            }

            parameters.AddParameter(node.Name, node.InnerText);
        }

        var responseSign = _signatureGenerator.Generate(parameters, MD5.Create(), options.ApiKey);

        return Task.FromResult(responseSign == weChatRequestXmlData.SelectSingleNode("/xml/sign")?.InnerText);
    }
}