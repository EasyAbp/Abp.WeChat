using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User
{
    public class UserTagDefinition
    {
        /// <summary>
        /// 标签的唯一 ID，由微信分配。
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 标签的名称，默认是 UTF-8 编码。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}