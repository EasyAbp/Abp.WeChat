using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class GetAllUserTagListResponse : OpenPlatformCommonResponse
{
    [JsonProperty("tags")]
    public List<UserTagDefinition> Tags { get; set; }
}