using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public interface IWeChatThirdPartyPlatformEventRequestHandlingService
{
    Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input);

    Task<WeChatRequestHandlingResult> NotifyAppAsync(NotifyAppInput input);
}