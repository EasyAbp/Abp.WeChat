using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Response
{
    public class BatchUnionUserInfoResponse : OfficialUserListResponse
    {
        [JsonPropertyName("user_info_list")]
        [JsonProperty("user_info_list")]
        public List<UnionUserInfoResponse> UserInfoList { get; private set; }
    }
}