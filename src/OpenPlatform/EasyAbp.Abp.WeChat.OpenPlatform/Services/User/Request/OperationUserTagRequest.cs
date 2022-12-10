using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public abstract class OperationUserTagRequest : OpenPlatformCommonRequest
{
    [JsonProperty("tag")] 
    public UserTagDefinition Tag { get; protected set; }
}