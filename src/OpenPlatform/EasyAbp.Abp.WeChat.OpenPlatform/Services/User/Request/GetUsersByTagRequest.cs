using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class GetUsersByTagRequest : OpenPlatformCommonRequest
{
    [JsonProperty("tagid")] public long TagId { get; protected set; }

    [JsonProperty("next_openid")] public string FirstOpenId { get; protected set; }

    public GetUsersByTagRequest(long tagId, string firstOpenId)
    {
        TagId = tagId;
        FirstOpenId = firstOpenId;
    }
}