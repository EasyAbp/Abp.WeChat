using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

public class AuthEventModel : ExtensibleObject
{
    public string AppId => this.GetProperty<string>("AppId");

    public int CreateTime => this.GetProperty<int>("CreateTime");

    public string InfoType => this.GetProperty<string>("InfoType");

    public string AuthorizerAppId => this.GetProperty<string>("AuthorizerAppid");

    public string AuthorizationCode => this.GetProperty<string>("AuthorizationCode");

    public int AuthorizationCodeExpiredTime => this.GetProperty<int>("AuthorizationCodeExpiredTime");

    public string PreAuthCode => this.GetProperty<string>("PreAuthCode");

    public string ComponentVerifyTicket => this.GetProperty<string>("ComponentVerifyTicket");
}