using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class BatchUnBlackListRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 要取消拉黑的用户的 openid 列表。
        /// </summary>
        [JsonPropertyName("openid_list")]
        [JsonProperty("openid_list")]
        public List<string> OpenIds { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="BatchUnBlackListRequest"/> 对象。
        /// </summary>
        /// <param name="openIds">要取消拉黑的用户的 openid 列表。</param>
        public BatchUnBlackListRequest(List<string> openIds)
        {
            OpenIds = openIds;
        }
    }
}