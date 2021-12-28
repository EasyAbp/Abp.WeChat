using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class CreateUserTagResponse : OfficialCommonResponse
    {
        /// <summary>
        /// 创建成功的标签。
        /// </summary>
        [JsonProperty("tag")]
        public UserTagDefinition Tag { get; set; }
    }
}