using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage
{
    /// <summary>
    /// 小程序订阅消息服务。
    /// </summary>
    public class SubscribeMessageWeService : MiniProgramAbpWeChatServiceBase
    {
        public SubscribeMessageWeService(AbpWeChatMiniProgramOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
        {
        }

        /// <summary>
        /// 发送小程序订阅消息
        /// </summary>
        /// <param name="toUser">接收者（用户）的 openid</param>
        /// <param name="templateId">所需下发的订阅模板id</param>
        /// <param name="page">点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。</param>
        /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }</param>
        /// <param name="miniProgramState">跳转小程序类型：developer为开发版；trial为体验版；formal为正式版；默认为正式版</param>
        /// <param name="lang">进入小程序查看”的语言类型，支持zh_CN(简体中文)、en_US(英文)、zh_HK(繁体中文)、zh_TW(繁体中文)，默认为zh_CN</param>
        public virtual Task<SendSubscribeMessageResponse> SendAsync(
            [NotNull] string toUser,
            [NotNull] string templateId,
            [CanBeNull] string page,
            [NotNull] SubscribeMessageData data,
            [CanBeNull] string miniProgramState = null,
            [CanBeNull] string lang = null)
        {
            const string targetUrl = "https://api.weixin.qq.com/cgi-bin/message/subscribe/send";

            var request = new SendSubscribeMessageRequest(toUser, templateId, page, data, miniProgramState, lang);

            return ApiRequester.RequestAsync<SendSubscribeMessageResponse>(
                targetUrl, HttpMethod.Post, request, Options);
        }
    }
}