using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetUsersByTagRequest : OfficialCommonRequest
    {
        [JsonPropertyName("tagid")]
        [JsonProperty("tagid")]
        public long TagId { get; protected set; }

        [JsonPropertyName("next_openid")]
        [JsonProperty("next_openid")]
        public string FirstOpenId { get; protected set; }

        public GetUsersByTagRequest(long tagId, string firstOpenId)
        {
            TagId = tagId;
            FirstOpenId = firstOpenId;
        }
    }
}