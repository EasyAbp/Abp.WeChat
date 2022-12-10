using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;

public class AuthNotificationModel : ExtensibleObject
{
    public string AppId => this.GetProperty<string>("AppId");

    public int CreateTime => this.GetProperty<int>("CreateTime");

    public string InfoType => this.GetProperty<string>("InfoType");

    public string AuthorizerAppid => this.GetProperty<string>("AuthorizerAppid");

    public string AuthorizationCode => this.GetProperty<string>("AuthorizationCode");

    public int AuthorizationCodeExpiredTime => this.GetProperty<int>("AuthorizationCodeExpiredTime");

    public string PreAuthCode => this.GetProperty<string>("PreAuthCode");
}