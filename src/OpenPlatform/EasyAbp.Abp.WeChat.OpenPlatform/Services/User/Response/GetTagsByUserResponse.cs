using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class GetTagsByUserResponse : OpenPlatformCommonResponse
{
    [JsonProperty("tagid_list")]
    public List<string> TagIds { get; set; }
}