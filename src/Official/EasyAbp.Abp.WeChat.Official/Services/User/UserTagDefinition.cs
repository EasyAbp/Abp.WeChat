using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User
{
    public class UserTagDefinition
    {
        /// <summary>
        /// 标签的唯一 ID，由微信分配。
        /// </summary>
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// 标签的名称，默认是 UTF-8 编码。
        /// </summary>
        [JsonPropertyName("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 标签下面的用户数量。
        /// </summary>
        [JsonPropertyName("count")]
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}