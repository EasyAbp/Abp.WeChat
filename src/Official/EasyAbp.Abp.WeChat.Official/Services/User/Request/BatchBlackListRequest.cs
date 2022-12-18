using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class BatchBlackListRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 要拉黑的用户的 openid 列表。
        /// </summary>
        [JsonProperty("openid_list")]
        public List<string> OpenIds { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="BatchBlackListRequest"/> 对象。
        /// </summary>
        /// <param name="openIds">要拉黑的用户的 openid 列表。</param>
        public BatchBlackListRequest(List<string> openIds)
        {
            OpenIds = openIds;
        }
    }
}