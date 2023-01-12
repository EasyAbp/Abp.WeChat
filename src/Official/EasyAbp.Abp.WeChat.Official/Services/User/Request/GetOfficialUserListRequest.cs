using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetOfficialUserListRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。
        /// </summary>
        [JsonPropertyName("next_openid")]
        [JsonProperty("next_openid")]
        public string FirstOpenId { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="GetOfficialUserListRequest"/> 对象。
        /// </summary>
        /// <param name="firstOpenId">起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。</param>
        public GetOfficialUserListRequest(string firstOpenId = null)
        {
            FirstOpenId = firstOpenId;
        }
    }
}