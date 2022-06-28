using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.Login
{
    /// <summary>
    /// 小程序手机号服务。
    /// </summary>
    public class PhoneNumberService : CommonService
    {
        public PhoneNumberService()
        {
        }

        /// <summary>
        /// 登录凭证校验。通过 wx.login 接口获得临时登录凭证 code 后传到开发者服务器调用此接口完成登录流程。
        /// </summary>
        /// <param name="appId">小程序 appId</param>
        /// <param name="appSecret">小程序 appSecret</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public Task<GetPhoneNumberResponse> GetPhoneNumberAsync(string code)
        {
            const string targetUrl = "https://api.weixin.qq.com/wxa/business/getuserphonenumber";

            var request = new GetPhoneNumberRequest(code);

            return WeChatMiniProgramApiRequester.RequestAsync<GetPhoneNumberResponse>(targetUrl, HttpMethod.Post, request);
        }
    }
}