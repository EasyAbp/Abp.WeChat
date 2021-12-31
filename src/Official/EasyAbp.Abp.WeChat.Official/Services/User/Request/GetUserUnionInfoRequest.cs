using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class GetUserUnionInfoRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 普通用户的标识，对当前公众号唯一。
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; protected set; }

        /// <summary>
        /// 返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语。
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="GetUserUnionInfoRequest"/> 对象。
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一。</param>
        /// <param name="language">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语。</param>
        public GetUserUnionInfoRequest(string openId, string language = null)
        {
            OpenId = openId;
            Language = language;
        }
    }
}