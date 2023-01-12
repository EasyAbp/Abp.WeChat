using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber
{
    /// <summary>
    /// 获取手机号时，需要传递的请求参数。
    /// </summary>
    public class GetPhoneNumberRequest : MiniProgramCommonRequest
    {
        /// <summary>
        /// 小程序获取的 code
        /// </summary>
        [JsonPropertyName("code")]
        [JsonProperty("code")]
        public string Code { get; protected set; }

        protected GetPhoneNumberRequest()
        {

        }

        /// <summary>
        /// 构造一个新的 <see cref="GetPhoneNumberRequest"/> 实例。
        /// </summary>
        /// <param name="code">小程序获取的 code</param>
        public GetPhoneNumberRequest(string code)
        {
            Code = code;
        }
    }
}
