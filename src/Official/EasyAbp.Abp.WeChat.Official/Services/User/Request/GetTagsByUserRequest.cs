using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetTagsByUserRequest : OfficialCommonRequest
    {
        [JsonPropertyName("openid")]
        [JsonProperty("openid")] 
        public string OpenId { get; protected set; }

        public GetTagsByUserRequest(string openId)
        {
            OpenId = openId;
        }
    }
}