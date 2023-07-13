using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public interface IWeChatOfficialEventRequestHandlingService
{
    Task<AppEventHandlingResult> NotifyAsync(WeChatOfficialEventRequestModel input, [CanBeNull] string appId);

    [Obsolete("请使用统一的Notify接口")]
    Task<StringValueWeChatRequestHandlingResult> VerifyAsync(VerifyRequestDto input, [CanBeNull] string appId);

    Task<StringValueWeChatRequestHandlingResult> GetOAuthRedirectUrlAsync(
        RedirectUrlRequest input, [CanBeNull] string appId);
}