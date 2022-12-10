using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class GetTagsByUserRequest : OpenPlatformCommonRequest
{
    [JsonProperty("openid")] 
    public string OpenId { get; protected set; }

    public GetTagsByUserRequest(string openId)
    {
        OpenId = openId;
    }
}