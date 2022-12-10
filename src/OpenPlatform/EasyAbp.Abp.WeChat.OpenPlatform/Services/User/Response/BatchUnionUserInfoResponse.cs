using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class BatchUnionUserInfoResponse : OpenPlatformUserListResponse
{
    [JsonProperty("user_info_list")]
    public List<UnionUserInfoResponse> UserInfoList { get; private set; }
}