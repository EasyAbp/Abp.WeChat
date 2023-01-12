using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetTagsByUserResponse : OfficialCommonResponse
    {
        [JsonPropertyName("tagid_list")]
        [JsonProperty("tagid_list")]
        public List<string> TagIds { get; set; }
    }
}