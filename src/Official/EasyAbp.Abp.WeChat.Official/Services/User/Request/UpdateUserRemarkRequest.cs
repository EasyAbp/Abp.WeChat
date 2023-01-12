﻿using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    internal class UpdateUserRemarkRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 微信公众号的用户唯一标识。
        /// </summary>
        [JsonPropertyName("openid")]
        [JsonProperty("openid")]
        public string OpenId { get; protected set; }

        /// <summary>
        /// 新的备注名，长度必须小于 30 字符。
        /// </summary>
        [JsonPropertyName("remark")]
        [JsonProperty("remark")]
        public string Remark { get; protected set; }

        /// <summary>
        /// 构造一个新的 <see cref="UpdateUserRemarkRequest"/> 对象。
        /// </summary>
        /// <param name="openId">微信公众号的用户唯一标识。</param>
        /// <param name="remark">新的备注名，长度必须小于 30 字符。</param>
        internal UpdateUserRemarkRequest(string openId, string remark)
        {
            OpenId = openId;
            Remark = remark;
        }
    }
}