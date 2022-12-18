using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public interface IWeChatOfficialEventRequestHandlingService
{
    Task<StringValueWeChatRequestHandlingResult> VerifyAsync(VerifyRequestDto input, [CanBeNull] string appId);

    Task<StringValueWeChatRequestHandlingResult> GetOAuthRedirectUrlAsync(
        RedirectUrlRequest input, [CanBeNull] string appId);
}