using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Official.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public class WeChatOfficialEventRequestHandlingService :
    WeChatEventRequestHandlingServiceBase<AbpWeChatOfficialOptions>,
    IWeChatOfficialEventRequestHandlingService, ITransientDependency
{
    private readonly ISignatureChecker _signatureChecker;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> _optionsProvider;
    private readonly IWeChatOfficialAppEventHandlerResolver _weChatOfficialAppEventHandlerResolver;

    public WeChatOfficialEventRequestHandlingService(
        ISignatureChecker signatureChecker,
        IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> optionsProvider,
        IWeChatOfficialAppEventHandlerResolver weChatOfficialAppEventHandlerResolver,
        IWeChatNotificationEncryptor weChatNotificationEncryptor) : base(weChatNotificationEncryptor)
    {
        _signatureChecker = signatureChecker;
        _optionsProvider = optionsProvider;
        _weChatOfficialAppEventHandlerResolver = weChatOfficialAppEventHandlerResolver;
    }

    /// <summary>
    /// 微信应用事件通知接口，开发人员需要实现 <see cref="IWeChatOfficialAppEventHandler"/> 处理器来处理回调请求。
    /// </summary>
    public virtual async Task<AppEventHandlingResult> NotifyAsync(WeChatOfficialEventRequestModel input, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        if (!input.EchoStr.IsNullOrWhiteSpace())
        {
            if (await ValidateSignatureAsync(options.Token, input.Timestamp, input.Nonce, input.MsgSignature))
            {
                return new AppEventHandlingResult(new PlainTextResponseToWeChatModel { Content = input.EchoStr });
            }

            return new AppEventHandlingResult(new PlainTextResponseToWeChatModel { Content = "非法参数。" });
        }

        var model = await DecryptMsgAsync<WeChatAppEventModel>(options, input);

        IResponseToWeChatModel responseToWeChatModel = null;

        foreach (var handler in (await _weChatOfficialAppEventHandlerResolver.GetAppEventHandlersAsync(model.MsgType))
                 .OrderByDescending(x => x.Priority))
        {
            var result = await handler.HandleAsync(options.AppId, model);

            if (result.ResponseToWeChatModel != null)
            {
                responseToWeChatModel = result.ResponseToWeChatModel;
            }

            if (!result.Success)
            {
                return result;
            }
        }

        return responseToWeChatModel is null
            ? new AppEventHandlingResult(true)
            : new AppEventHandlingResult(responseToWeChatModel);
    }

    [Obsolete("请使用统一的Notify接口")]
    public virtual async Task<StringValueWeChatRequestHandlingResult> VerifyAsync(VerifyRequestDto input, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        if (await ValidateSignatureAsync(options.Token, input.Timestamp, input.Nonce, input.Signature))
        {
            return new StringValueWeChatRequestHandlingResult(true, input.EchoStr);
        }

        return new StringValueWeChatRequestHandlingResult(false, null, "非法参数。");
    }

    protected virtual Task<bool> ValidateSignatureAsync(string token, string timestamp, string nonce, string signature)
    {
        return Task.FromResult(_signatureChecker.Validate(token, timestamp, nonce, signature));
    }

    public virtual async Task<StringValueWeChatRequestHandlingResult> GetOAuthRedirectUrlAsync(
        RedirectUrlRequest input, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        return new StringValueWeChatRequestHandlingResult(true,
            $"{options.OAuthRedirectUrl.TrimEnd('/')}?code={input.Code}");
    }
}