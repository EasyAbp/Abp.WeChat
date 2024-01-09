using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventRequestHandlingService : IWeChatPayEventRequestHandlingService, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    private readonly IAbpWeChatPayOptionsProvider _optionsProvider;
    private readonly IPlatformCertificateManager _platformCertificateManager;

    public WeChatPayEventRequestHandlingService(
        IAbpWeChatPayOptionsProvider optionsProvider,
        IPlatformCertificateManager platformCertificateManager)
    {
        _optionsProvider = optionsProvider;
        _platformCertificateManager = platformCertificateManager;
    }

    public virtual async Task<WeChatRequestHandlingResult> PaidNotifyAsync(PaidNotifyInput input)
    {
        var options = await _optionsProvider.GetAsync(input.MchId);

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayEventHandler>>()
            .Where(h => h.Type == WeChatHandlerType.Paid);

        if (!await IsSignValidAsync(input, options))
        {
            return new WeChatRequestHandlingResult(false, "签名验证不通过");
        }

        var decryptingResult = DecryptResource<QueryOrderResponse>(input, options);

        var model = new WeChatPayEventModel<QueryOrderResponse>
        {
            Options = options,
            Resource = decryptingResult
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

    public virtual async Task<WeChatRequestHandlingResult> RefundNotifyAsync(RefundNotifyInput input)
    {
        var options = await _optionsProvider.GetAsync(input.MchId);

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayEventHandler>>()
            .Where(x => x.Type == WeChatHandlerType.Refund);

        // if (!decryptingResult)
        // {
        //     return new WeChatRequestHandlingResult(false, "微信消息体解码失败");
        // }

        var model = new WeChatPayEventModel<QueryOrderResponse>
        {
            Options = options
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

    protected virtual async Task<bool> IsSignValidAsync(PaidNotifyInput input, AbpWeChatPayOptions options)
    {
        var certificate = await _platformCertificateManager.GetPlatformCertificateAsync(options.MchId, input.SerialNumber);
        var sb = new StringBuilder();
        sb.Append(input.Timestamp).Append("\n")
            .Append(input.Nonce).Append("\n")
            .Append(input.RequestBodyString).Append("\n");
        return certificate.VerifySignature(sb.ToString(), input.Signature);
    }

    protected virtual TObject DecryptResource<TObject>(PaidNotifyInput input, AbpWeChatPayOptions options)
    {
        var sourceJson = WeChatPaySecurityUtility.AesGcmDecrypt(options.ApiV3Key, input.RequestBody.Resource.AssociatedData,
            input.RequestBody.Resource.Nonce, input.RequestBody.Resource.Ciphertext);
        return JsonConvert.DeserializeObject<TObject>(sourceJson);
    }
}