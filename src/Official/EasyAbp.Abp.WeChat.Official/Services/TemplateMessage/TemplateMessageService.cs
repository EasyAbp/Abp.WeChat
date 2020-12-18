using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.TemplateMessage
{
    /// <summary>
    /// 模板消息服务，可以注入本服务来发送微信模板消息。
    /// </summary>
    public class TemplateMessageService : CommonService
    {
        private const string SendUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?";
        private const string SetIndustryUrl = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?";
        private const string GetIndustryUrl = "https://api.weixin.qq.com/cgi-bin/template/get_industry?";
        private const string GetTemplateIdUrl = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?";
        private const string GetAllPrivateTemplateUrl = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?";
        private const string DeletePrivateTemplateUrl = "https://api.weixin.qq.com/cgi-bin/template/del_private_template?";

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
            return WeChatOfficialApiRequester.RequestAsync<SendMessageResponse>(SendUrl,
                HttpMethod.Post,
                new SendMessageRequest(openId,
                    templateId,
                    targetUrl,
                    templateMessage,
                    miniProgramRequest));
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
            return SendMessageAsync(openId,
                templateId,
                targetUrl,
                JsonConvert.DeserializeObject<TemplateMessage>(templateMessage),
                miniProgramRequest);
        }

        /// <summary>
        /// 设置模版消息的所属行业。<br/>
        /// 具体的行业代码可以参考 https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Template_Message_Interface.html#0。
        /// </summary>
        /// <remarks>
        /// 设置行业可在微信公众平台后台完成，每月可修改行业 1 次。<br/>
        /// 帐号仅可使用所属行业中相关的模板。
        /// </remarks>
        /// <param name="primaryIndustry">公众号模板消息所属行业编号。</param>
        /// <param name="secondaryIndustry">公众号模板消息所属行业编号。</param>
        public Task<OfficialCommonResponse> SetIndustryAsync(string primaryIndustry, string secondaryIndustry)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(SetIndustryUrl,
                HttpMethod.Post,
                new SetIndustryRequest(primaryIndustry, secondaryIndustry));
        }

        /// <summary>
        /// 获取设置的模版消息行业信息。
        /// </summary>
        /// <remarks>
        /// 获取帐号设置的行业信息。可登录微信公众平台，在公众号后台中查看行业信息。
        /// </remarks>
        public Task<GetIndustryResponse> GetIndustryAsync()
        {
            return WeChatOfficialApiRequester.RequestAsync<GetIndustryResponse>(GetIndustryUrl,
                HttpMethod.Get);
        }

        /// <summary>
        /// 根据短模版 Id 创建模版。
        /// </summary>
        /// <param name="templateShortId">模板库中模板的编号，有 "TM**" 和 "OPENTMTM**" 等形式。</param>
        public Task<CreateTemplateResponse> CreateTemplateAsync(string templateShortId)
        {
            return WeChatOfficialApiRequester.RequestAsync<CreateTemplateResponse>(GetTemplateIdUrl,
                HttpMethod.Post,
                new CreateTemplateRequest(templateShortId));
        }

        /// <summary>
        /// 获取已添加至账号下所有模版列表。
        /// </summary>
        public Task<GetAllPrivateTemplateResponse> GetAllPrivateTemplateAsync()
        {
            return WeChatOfficialApiRequester.RequestAsync<GetAllPrivateTemplateResponse>(GetAllPrivateTemplateUrl,
                HttpMethod.Get);
        }

        /// <summary>
        /// 根据模版 Id 删除指定的模版。
        /// </summary>
        /// <param name="templateId">公众帐号下模板消息 ID。</param>
        public Task<OfficialCommonResponse> DeleteTemplateAsync(string templateId)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(DeletePrivateTemplateUrl,
                HttpMethod.Post,
                new DeleteTemplateRequest(templateId));
        }
    }
}