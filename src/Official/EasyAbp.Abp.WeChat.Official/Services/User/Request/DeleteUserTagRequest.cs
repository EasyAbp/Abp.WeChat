using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class DeleteUserTagRequest : OfficialCommonRequest
    {
        [JsonProperty("tag")] 
        public UserTagDefinition Tag { get; protected set; }

        public DeleteUserTagRequest(long tagId)
        {
            Tag = new UserTagDefinition
            {
                Id = tagId
            };
        }
    }
}