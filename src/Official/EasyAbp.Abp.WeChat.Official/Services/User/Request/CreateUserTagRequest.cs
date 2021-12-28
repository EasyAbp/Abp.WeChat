using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class CreateUserTagRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 需要创建的标签定义。
        /// </summary>
        [JsonProperty("tag")]
        public UserTagDefinition Tag { get; set; }

        public CreateUserTagRequest(string name)
        {
            Tag = new UserTagDefinition
            {
                Name = name
            };
        }
    }
}