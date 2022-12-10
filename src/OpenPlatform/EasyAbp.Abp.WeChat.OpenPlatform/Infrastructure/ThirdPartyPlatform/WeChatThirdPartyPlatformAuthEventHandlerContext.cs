using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;

public class WeChatThirdPartyPlatformAuthEventHandlerContext
{
    public AuthNotificationModel Model { get; set; }

    public bool IsSuccess { get; set; } = true;

    public string FailedResponse { get; set; } = null;
}