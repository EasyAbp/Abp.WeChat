using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetAllUserTagListResponse : OfficialCommonResponse
    {
        [JsonPropertyName("tags")]
        [JsonProperty("tags")]
        public List<UserTagDefinition> Tags { get; set; }
    }
}