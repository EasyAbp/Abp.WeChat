using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class BatchTaggingRequest : OfficialCommonRequest
    {
        [JsonProperty("tagid")]
        public long TagId { get; protected set; }

        [JsonProperty("openid_list")]
        public List<string> OpenIds { get; protected set; }

        public BatchTaggingRequest(long tagId, List<string> openIds)
        {
            TagId = tagId;
            OpenIds = openIds;
        }
    }
}