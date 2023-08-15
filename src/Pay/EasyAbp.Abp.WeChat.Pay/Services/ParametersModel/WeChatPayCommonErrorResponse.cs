using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;

public abstract class WeChatPayCommonErrorResponse
{
    /// <summary>
    /// 错误代码，具体定义可以参考 <see cref="ErrorCodes"/> 命名空间下的常量定义。
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// 具体的错误信息。
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    public class InnerDetail
    {
        /// <summary>
        /// 指示错误参数的位置。
        /// </summary>
        /// <remarks>
        /// 当错误参数位于请求 Body 的 JSON 时，展示的只想参数的 JSON Pointer。<br/>
        /// 当错误参数位于请求的 Url 或者 QueryString 时，展示的参数的变量名。
        /// </remarks>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// 错误的值。
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// 具体错误原因。
        /// </summary>
        [JsonProperty("issue")]
        public string Issue { get; set; }

        /// <summary>
        /// 错误的位置，例如：body、query、path。
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}