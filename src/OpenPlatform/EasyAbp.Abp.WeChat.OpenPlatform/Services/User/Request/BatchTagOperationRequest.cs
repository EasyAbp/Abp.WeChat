using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class BatchTagOperationRequest : OpenPlatformCommonRequest
{
    [JsonProperty("tagid")]
    public long TagId { get; protected set; }

    [JsonProperty("openid_list")]
    public List<string> OpenIds { get; protected set; }

    public BatchTagOperationRequest(long tagId, List<string> openIds)
    {
        TagId = tagId;
        OpenIds = openIds;
    }
}