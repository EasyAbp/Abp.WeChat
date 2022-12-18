namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class WeChatThirdPartyPlatformInfo
{
    public string ComponentAppId { get; set; }

    public WeChatThirdPartyPlatformInfo(string componentAppId)
    {
        ComponentAppId = componentAppId;
    }
}