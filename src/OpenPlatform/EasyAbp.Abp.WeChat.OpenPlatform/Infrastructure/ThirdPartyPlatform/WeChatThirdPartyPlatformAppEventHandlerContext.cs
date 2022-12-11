using EasyAbp.Abp.WeChat.Common.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;

public class WeChatThirdPartyPlatformAppEventHandlerContext
{
    public string ComponentAppId { get; set; }
    
    public string AuthorizerAppId { get; set; }
    
    public WeChatAppNotificationModel Model { get; set; }

    public bool IsSuccess { get; set; } = true;

    public string FailedResponse { get; set; } = null;
}