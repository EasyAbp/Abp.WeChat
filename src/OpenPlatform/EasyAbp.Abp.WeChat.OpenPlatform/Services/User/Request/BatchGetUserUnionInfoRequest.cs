using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

internal class BatchGetUserUnionInfoRequest : OpenPlatformCommonRequest
{
    /// <summary>
    /// 需要查询的用户 OPENID 列表，最多支持 100 个。
    /// </summary>
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