using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetBlackListRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。
        /// </summary>
        [JsonProperty("begin_openid")]
        public string BeginOpenId { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="GetBlackListRequest"/> 对象。
        /// </summary>
        /// <param name="beginOpenId">起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。</param>
        public GetBlackListRequest(string beginOpenId)
        {
            BeginOpenId = beginOpenId;
        }
    }
}