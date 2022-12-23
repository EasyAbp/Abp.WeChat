using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public interface IWeChatThirdPartyPlatformEventRequestHandlingService
{
    Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input);

    Task<AppEventHandlingResult> NotifyAppAsync(NotifyAppInput input);
}