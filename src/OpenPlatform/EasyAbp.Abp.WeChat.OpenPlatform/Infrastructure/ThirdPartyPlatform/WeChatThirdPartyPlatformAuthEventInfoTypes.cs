namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;

public static class WeChatThirdPartyPlatformAuthEventInfoTypes
{
    /// <summary>
    /// 验证票据
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/component_verify_ticket.html
    /// </summary>
    public static string ComponentVerifyTicket = "component_verify_ticket";

    /// <summary>
    /// 取消授权
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    public static string Unauthorized = "unauthorized";

    /// <summary>
    /// 更新授权
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    public static string UpdateAuthorized = "updateauthorized";

    /// <summary>
    /// 授权成功
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    public static string Authorized = "authorized";
}