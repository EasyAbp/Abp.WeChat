namespace EasyAbp.Abp.WeChat.MiniProgram.Settings;

public static class AbpWeChatMiniProgramSettings
{
    private const string GroupName = "EasyAbp.Abp.WeChat.MiniProgram";

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