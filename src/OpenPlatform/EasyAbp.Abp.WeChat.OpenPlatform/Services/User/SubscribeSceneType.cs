namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User;

/// <summary>
/// 用户关注的渠道来源定义。
/// </summary>
public static class SubscribeSceneType
{
    /// <summary>
    /// 公众号搜索。
    /// </summary>
    public const string AddSceneSearch = "ADD_SCENE_SEARCH";

    /// <summary>
    /// 公众号迁移。
    /// </summary>
    public const string AddSceneAccountMigration = "ADD_SCENE_ACCOUNT_MIGRATION";

    /// <summary>
    /// 名片分享。
    /// </summary>
    public const string AddSceneProfileCard = "ADD_SCENE_PROFILE_CARD";

    /// <summary>
    /// 扫描二维码。
    /// </summary>
    public const string AddSceneQrCode = "ADD_SCENE_QR_CODE";

    /// <summary>
    /// 图文页内名称点击。
    /// </summary>
    public const string AddSceneProfileLink = "ADD_SCENE_PROFILE_LINK";

    /// <summary>
    /// 图文页右上角菜单。
    /// </summary>
    public const string AddSceneProfileItem = "ADD_SCENE_PROFILE_ITEM";

    /// <summary>
    /// 支付后关注。
    /// </summary>
    public const string AddScenePaid = "ADD_SCENE_PAID";

    /// <summary>
    /// 微信广告。
    /// </summary>
    public const string AddSceneWechatAdvertisement = "ADD_SCENE_WECHAT_ADVERTISEMENT";

    /// <summary>
    /// 他人转载。
    /// </summary>
    public const string AddSceneReprint = "ADD_SCENE_REPRINT";

    /// <summary>
    /// 视频号直播。
    /// </summary>
    public const string AddSceneLivestream = "ADD_SCENE_LIVESTREAM";

    /// <summary>
    /// 视频号。
    /// </summary>
    public const string AddSceneChannels = "ADD_SCENE_CHANNELS";

    /// <summary>
    /// 其他。
    /// </summary>
    public const string AddSceneOthers = "ADD_SCENE_OTHERS";
}