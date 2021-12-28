using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class GetTagsByUserResponse : OfficialCommonResponse
    {
        [JsonProperty("tagid_list")]
        public List<string> TagIds { get; set; }
    }
}