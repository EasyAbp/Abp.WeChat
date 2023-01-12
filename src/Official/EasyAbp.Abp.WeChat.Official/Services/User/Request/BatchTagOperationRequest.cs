using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class BatchTagOperationRequest : OfficialCommonRequest
    {
        [JsonPropertyName("tagid")]
        [JsonProperty("tagid")]
        public long TagId { get; protected set; }

        [JsonPropertyName("openid_list")]
        [JsonProperty("openid_list")]
        public List<string> OpenIds { get; protected set; }

        public BatchTagOperationRequest(long tagId, List<string> openIds)
        {
            TagId = tagId;
            OpenIds = openIds;
        }
    }
}