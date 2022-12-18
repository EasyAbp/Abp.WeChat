using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public class WeChatOfficialEventRequestHandlingService : IWeChatOfficialEventRequestHandlingService,
    ITransientDependency
{
    private readonly ISignatureChecker _signatureChecker;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> _optionsProvider;

    public WeChatOfficialEventRequestHandlingService(
        ISignatureChecker signatureChecker,
        IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> optionsProvider)
    {
        _signatureChecker = signatureChecker;
        _optionsProvider = optionsProvider;
    }

    public virtual async Task<StringValueWeChatRequestHandlingResult> VerifyAsync(VerifyRequestDto input, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        if (_signatureChecker.Validate(options.Token, input.Timestamp, input.Nonce, input.Signature))
        {
            return new StringValueWeChatRequestHandlingResult(true, input.EchoStr);
        }

        return new StringValueWeChatRequestHandlingResult(false, null, "非法参数。");
    }

    public virtual async Task<StringValueWeChatRequestHandlingResult> GetOAuthRedirectUrlAsync(
        RedirectUrlRequest input, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        return new StringValueWeChatRequestHandlingResult(true,
            $"{options.OAuthRedirectUrl.TrimEnd('/')}?code={input.Code}");
    }
}