using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class UnionUserInfoResponse : OpenPlatformCommonResponse
{
    /// <summary>
    /// 用户是否订阅该公众号标识，值为 0 时，代表此用户没有关注该公众号，拉取不到其余信息。
    /// </summary>
    [JsonProperty("subscribe")]
    public string Subscribe { get; private set; }

    /// <summary>
    /// 用户的标识，对当前公众号唯一。
    /// </summary>
    [JsonProperty("openid")]
    public string OpenId { get; private set; }

    /// <summary>
    /// 用户的语言，zh_CN 简体，zh_TW 繁体，en 英语。
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; private set; }

    /// <summary>
    /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间。
    /// </summary>
    [JsonProperty("subscribe_time")]
    public long SubscribeTime { get; private set; }

    /// <summary>
    /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
    /// </summary>
    [JsonProperty("unionid")]
    public string UniqueId { get; private set; }

    /// <summary>
    /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注。
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; private set; }

    /// <summary>
    /// 用户所在的分组 ID（兼容旧的用户分组接口）。
    /// </summary>
    [JsonProperty("groupid")]
    public long GroupId { get; private set; }

    /// <summary>
    /// 用户被打上的标签 ID 列表。
    /// </summary>
    [JsonProperty("tagid_list")]
    public List<string> TagIdsList { get; private set; }

    /// <summary>
    /// 返回用户关注的渠道来源，具体值参考 <see cref="SubscribeSceneType"/> 类型的定义。
    /// </summary>
    [JsonProperty("subscribe_scene")]
    public string SubscribeScene { get; private set; }

    /// <summary>
    /// 二维码扫码场景（开发者自定义）。
    /// </summary>
    [JsonProperty("qr_scene")]
    public int QrScene { get; private set; }

    /// <summary>
    /// 二维码扫码场景描述（开发者自定义）。
    /// </summary>
    [JsonProperty("qr_scene_str")]
    public string QrSceneStr { get; private set; }
}