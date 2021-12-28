using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetTagsByUserRequest : OfficialCommonRequest
    {
        [JsonProperty("openid")] 
        public string OpenId { get; protected set; }

        public GetTagsByUserRequest(string openId)
        {
            OpenId = openId;
        }
    }
}