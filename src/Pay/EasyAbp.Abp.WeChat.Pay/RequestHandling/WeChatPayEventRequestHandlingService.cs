using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;
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

    public virtual async Task<WeChatRequestHandlingResult> PaidNotifyAsync(NotifyInputDto input)
    {
        var options = await _optionsProvider.GetAsync(input.MchId);

        if (!await IsSignValidAsync(input, options))
        {
            return new WeChatRequestHandlingResult(false, "签名验证不通过");
        }

        var handlers = LazyServiceProvider
            .LazyGetService<IEnumerable<IWeChatPayEventHandler<WeChatPayPaidEventModel>>>()
            .Where(h => h.Type == WeChatHandlerType.Paid);

        var decryptingResult = DecryptResource<WeChatPayPaidEventModel>(input, options);

        var model = new WeChatPayEventModel<WeChatPayPaidEventModel>
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

    public virtual async Task<WeChatRequestHandlingResult> RefundNotifyAsync(NotifyInputDto input)
    {
        var options = await _optionsProvider.GetAsync(input.MchId);

        if (!await IsSignValidAsync(input, options))
        {
            return new WeChatRequestHandlingResult(false, "签名验证不通过");
        }

        var handlers = LazyServiceProvider
            .LazyGetService<IEnumerable<IWeChatPayEventHandler<WeChatPayRefundEventModel>>>()
            .Where(x => x.Type == WeChatHandlerType.Refund);

        var decryptingResult = DecryptResource<WeChatPayRefundEventModel>(input, options);
        var model = new WeChatPayEventModel<WeChatPayRefundEventModel>
        {
            Options = options,
            Resource = decryptingResult
        };

        foreach (var handler in handlers.Where(x => x.Type == WeChatHandlerType.Refund))
        {
            var result = await handler.HandleAsync(model);

            if (!result.Success)
            {
                return result;
            }
        }

        return new WeChatRequestHandlingResult(true);
    }

    protected virtual async Task<bool> IsSignValidAsync(NotifyInputDto inputDto, AbpWeChatPayOptions options)
    {
        var sb = new StringBuilder();
        sb.Append(inputDto.HttpHeader.Timestamp).Append("\n")
            .Append(inputDto.HttpHeader.Nonce).Append("\n")
            .Append(inputDto.RequestBodyString).Append("\n");

        if (inputDto.HttpHeader.SerialNumber.StartsWith("PUB_KEY_ID_"))
        {
            return WeChatPaySecurityUtility.Verify(sb.ToString(), inputDto.HttpHeader.Signature, options.PublicKey);
        }

        var certificate =
            await _platformCertificateManager.GetPlatformCertificateAsync(options.MchId,
                inputDto.HttpHeader.SerialNumber);
        return certificate.VerifySignature(sb.ToString(), inputDto.HttpHeader.Signature);
    }

    protected virtual TObject DecryptResource<TObject>(NotifyInputDto inputDto, AbpWeChatPayOptions options)
    {
        var sourceJson = WeChatPaySecurityUtility.AesGcmDecrypt(options.ApiV3Key,
            inputDto.RequestBody.Resource.AssociatedData,
            inputDto.RequestBody.Resource.Nonce, inputDto.RequestBody.Resource.Ciphertext);
        return JsonConvert.DeserializeObject<TObject>(sourceJson);
    }
}