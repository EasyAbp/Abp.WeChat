using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    internal class BatchGetUserUnionInfoRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 需要查询的用户 OPENID 列表，最多支持 100 个。
        /// </summary>
        [JsonPropertyName("user_list")]
        [JsonProperty("user_list")]
        public List<GetUserUnionInfoRequest> UserList { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="BatchGetUserUnionInfoRequest"/> 对象。
        /// </summary>
        /// <param name="userList">需要查询的用户 OPENID 列表，最多支持 100 个。</param>
        public BatchGetUserUnionInfoRequest(List<GetUserUnionInfoRequest> userList)
        {
            UserList = userList;
        }
    }
}