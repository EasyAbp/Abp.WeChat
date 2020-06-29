using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Services.Login
{
    /// <summary>
    /// 小程序登录服务。
    /// </summary>
    public class LoginService : CommonService
    {
        /// <summary>
        /// 请求微信公众号的 API 发送指定的模板消息。
        /// </summary>
        /// <param name="appId">小程序 appId</param>
        /// <param name="appSecret">小程序 appSecret</param>
        /// <param name="jsCode">登录时获取的 code</param>
        /// <param name="grantType">授权类型，此处只需填写 authorization_code</param>
        public Task<Code2SessionResponse> SendMessageAsync(string appId, string appSecret, string jsCode, string grantType = "authorization_code")
        {
            const string targetUrl = "https://api.weixin.qq.com/sns/jscode2session?";

            var request = new Code2SessionRequest(appId, appSecret, jsCode, grantType);

            return WeChatMiniProgramApiRequester.RequestAsync<Code2SessionResponse>(targetUrl, HttpMethod.Post, request);
        }
    }
}