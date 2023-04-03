namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Settings;

public static class AbpWeChatThirdPartyPlatformSettings
{
    private const string GroupName = "EasyAbp.Abp.WeChat.ThirdPartyPlatform";

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
}