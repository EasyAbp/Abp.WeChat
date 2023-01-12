using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage
{
    /// <summary>
    /// 发送订阅消息时，需要传递的请求参数。
    /// </summary>
    public class SendSubscribeMessageRequest : MiniProgramCommonRequest
    {
        /// <summary>
        /// 接收者（用户）的 openid
        /// </summary>
        [NotNull]
        [JsonPropertyName("touser")]
        [JsonProperty("touser")]
        public string ToUser { get; protected set; }

        /// <summary>
        /// 所需下发的订阅模板id
        /// </summary>
        [NotNull]
        [JsonPropertyName("template_id")]
        [JsonProperty("template_id")]
        public string TemplateId { get; protected set; }
        
        /// <summary>
        /// 点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。
        /// </summary>
        [CanBeNull]
        [JsonPropertyName("page")]
        [JsonProperty("page")]
        public string Page { get; protected set; }

        /// <summary>
        /// 模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }
        /// </summary>
        [NotNull]
        [JsonPropertyName("data")]
        [JsonProperty("data")]
        public SubscribeMessageData Data { get; protected set; }
        
        /// <summary>
        /// 跳转小程序类型：developer为开发版；trial为体验版；formal为正式版；默认为正式版
        /// </summary>
        [CanBeNull]
        [JsonPropertyName("miniprogram_state")]
        [JsonProperty("miniprogram_state")]
        public string MiniProgramState { get; protected set; }
        
        /// <summary>
        /// 进入小程序查看”的语言类型，支持zh_CN(简体中文)、en_US(英文)、zh_HK(繁体中文)、zh_TW(繁体中文)，默认为zh_CN
        /// </summary>
        [CanBeNull]
        [JsonPropertyName("lang")]
        [JsonProperty("lang")]
        public string Lang { get; protected set; }

        protected SendSubscribeMessageRequest()
        {
            
        }

        /// <summary>
        /// 构造一个新的 <see cref="SendSubscribeMessageRequest"/> 实例。
        /// </summary>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="page">点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="miniProgramState">跳转小程序类型：developer为开发版；trial为体验版；formal为正式版；默认为正式版</param>
        /// <param name="lang">进入小程序查看”的语言类型，支持zh_CN(简体中文)、en_US(英文)、zh_HK(繁体中文)、zh_TW(繁体中文)，默认为zh_CN</param>
        public SendSubscribeMessageRequest(
            [NotNull] string toUser,
            [NotNull] string templateId,
            [CanBeNull] string page,
            [NotNull] SubscribeMessageData data,
            [CanBeNull] string miniProgramState,
            [CanBeNull] string lang)
        {
            ToUser = toUser;
            TemplateId = templateId;
            Page = page;
            Data = data;
            MiniProgramState = miniProgramState;
            Lang = lang;
        }
    }
}