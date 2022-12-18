using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public abstract class OperationUserTagRequest : OfficialCommonRequest
    {
        [JsonProperty("tag")] 
        public UserTagDefinition Tag { get; protected set; }
    }
}