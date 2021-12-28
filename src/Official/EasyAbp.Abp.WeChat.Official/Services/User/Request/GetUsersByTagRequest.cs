using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetUsersByTagRequest : OfficialCommonRequest
    {
        [JsonProperty("tagid")] public long TagId { get; protected set; }

        [JsonProperty("next_openid")] public string FirstOpenId { get; protected set; }

        public GetUsersByTagRequest(long tagId, string firstOpenId)
        {
            TagId = tagId;
            FirstOpenId = firstOpenId;
        }
    }
}