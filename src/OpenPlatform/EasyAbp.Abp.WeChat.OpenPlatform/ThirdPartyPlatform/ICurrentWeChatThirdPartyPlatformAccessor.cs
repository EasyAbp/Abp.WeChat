using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;

public interface ICurrentWeChatThirdPartyPlatformAccessor
{
    WeChatThirdPartyPlatformInfo Current { get; set; }
}