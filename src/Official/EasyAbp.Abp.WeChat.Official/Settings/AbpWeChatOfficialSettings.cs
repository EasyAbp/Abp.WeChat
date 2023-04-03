namespace EasyAbp.Abp.WeChat.Official.Settings;

public static class AbpWeChatOfficialSettings
{
    private const string GroupName = "EasyAbp.Abp.WeChat.Official";

    public const string AppId = GroupName + ".AppId";

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public const string AppSecret = GroupName + ".AppSecret";

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public const string Token = GroupName + ".Token";

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public const string EncodingAesKey = GroupName + ".EncodingAesKey";

    public const string OAuthRedirectUrl = GroupName + ".OAuthRedirectUrl";
}