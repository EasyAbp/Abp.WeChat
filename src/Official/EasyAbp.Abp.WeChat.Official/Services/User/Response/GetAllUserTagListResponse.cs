using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetAllUserTagListResponse : OfficialCommonResponse
    {
        [JsonProperty("tags")]
        public List<UserTagDefinition> Tags { get; set; }
    }
}