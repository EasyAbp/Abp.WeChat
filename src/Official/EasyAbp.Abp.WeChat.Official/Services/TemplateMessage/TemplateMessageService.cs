using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 模板消息服务，可以注入本服务来发送微信模板消息。
    /// </summary>
    public class TemplateMessageService : CommonService
    {
        private const string SendUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";
        private const string SetIndustry = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?";

        /// <summary>
        /// 请求微信公众号的 API 发送指定的模板消息。
        /// </summary>
        /// <param name="openId">目标微信用户的 OpenId。</param>
        /// <param name="templateId">需要发送的模板消息 Id。</param>
        /// <param name="targetUrl">微信用户收到模板消息时，点击之后需要跳转的 Url。</param>
        /// <param name="templateMessage">需要发送的模板消息内容。</param>
        /// <param name="miniProgramRequest">模板关联的小程序参数，如果没有的话可以不用传递。</param>
        public Task<SendMessageResponse> SendMessageAsync(string openId,
            string templateId,
            string targetUrl,
            TemplateMessage templateMessage,
            MiniProgramRequest miniProgramRequest = null)
        {
            var request = new SendMessageRequest(openId,
                templateId,
                targetUrl,
                templateMessage,
                miniProgramRequest);

            return WeChatOfficialApiRequester.RequestAsync<SendMessageResponse>(SendUrl, HttpMethod.Post, request);
        }

        /// <summary>
        /// 请求微信公众号的 API 发送指定的模板消息。
        /// </summary>
        /// <param name="openId">目标微信用户的 OpenId。</param>
        /// <param name="templateId">需要发送的模板消息 Id。</param>
        /// <param name="targetUrl">微信用户收到模板消息时，点击之后需要跳转的 Url。</param>
        /// <param name="templateMessage">需要发送的模板消息内容。，这里的模板消息可以是用户提前存储的 JSON 字串。</param>
        /// <param name="miniProgramRequest">模板关联的小程序参数，如果没有的话可以不用传递。</param>
        public Task<SendMessageResponse> SendMessageAsync(string openId,
            string templateId,
            string targetUrl,
            string templateMessage,
            MiniProgramRequest miniProgramRequest = null)
        {
            return SendMessageAsync(openId, templateId, targetUrl, JsonConvert.DeserializeObject<TemplateMessage>(templateMessage), miniProgramRequest);
        }

        /// <summary>
        /// 设置模版消息的所属行业。<br/>
        /// 具体的行业代码可以参考 https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Template_Message_Interface.html#0。
        /// </summary>
        /// <remarks>
        /// 设置行业可在微信公众平台后台完成，每月可修改行业 1 次。<br/>
        /// 帐号仅可使用所属行业中相关的模板。
        /// </remarks>
        /// <param name="industry1">公众号模板消息所属行业编号。</param>
        /// <param name="industry2">公众号模板消息所属行业编号。</param>
        public Task<OfficialCommonResponse> SetIndustryAsync(string industry1, string industry2)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(SetIndustry, HttpMethod.Post, new SetIndustryRequest(industry1, industry2));
        }
    }
}