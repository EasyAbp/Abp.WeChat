using System.Net.Http;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber
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
        /// code换取用户手机号。 每个 code 只能使用一次，code的有效期为5min
        /// </summary>
        /// <param name="code">手机号获取凭证</param>
        public virtual Task<GetPhoneNumberResponse> GetPhoneNumberAsync(string code)
        {
            const string targetUrl = "https://api.weixin.qq.com/wxa/business/getuserphonenumber";

            var request = new GetPhoneNumberRequest(code);

            return WeChatMiniProgramApiRequester.RequestAsync<GetPhoneNumberResponse>(targetUrl, HttpMethod.Post, request);
        }
    }
}