using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class GetOpenPlatformUserListRequest : OpenPlatformCommonRequest
{
    /// <summary>
    /// 起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。
    /// </summary>
    [JsonProperty("next_openid")]
    public string FirstOpenId { get; protected set; }

    /// <summary>
    /// 构造一个新的 <see cref="GetOpenPlatformUserListRequest"/> 对象。
    /// </summary>
    /// <param name="firstOpenId">起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。</param>
    public GetOpenPlatformUserListRequest(string firstOpenId = null)
    {
        FirstOpenId = firstOpenId;
    }
}