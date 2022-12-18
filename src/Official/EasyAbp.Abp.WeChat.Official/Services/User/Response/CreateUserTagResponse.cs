using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
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