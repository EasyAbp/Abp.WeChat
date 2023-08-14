using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.ACode
{
    /// <summary>
    /// 获取 Unlimited 小程序码时，需要传递的请求参数。
    /// </summary>
    public class GetUnlimitedACodeRequest : MiniProgramCommonRequest
    {
        /// <summary>
        /// 最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）
        /// </summary>
        [JsonPropertyName("scene")]
        [JsonProperty("scene")]
        public string Scene { get; protected set; }

        /// <summary>
        /// 必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
        /// </summary>
        [JsonPropertyName("page")]
        [JsonProperty("page")]
        public string Page { get; protected set; }

        /// <summary>
        /// 检查 page 是否存在，为 true 时 page 必须是已经发布的小程序存在的页面（否则报错）；为 false 时允许小程序未发布或者 page 不存在， 但 page 有数量上限（60000个）请勿滥用
        /// </summary>
        [JsonPropertyName("check_path")]
        [JsonProperty("check_path")]
        public bool CheckPage { get; protected set; }

        /// <summary>
        /// 要打开的小程序版本。正式版为 release，体验版为 trial，开发版为 develop
        /// </summary>
        [JsonPropertyName("env_version")]
        [JsonProperty("env_version")]
        public string EnvVersion { get; protected set; }

        /// <summary>
        /// 二维码的宽度，单位 px，最小 280px，最大 1280px
        /// </summary>
        [JsonPropertyName("width")]
        [JsonProperty("width")]
        public short Width { get; protected set; }

        /// <summary>
        /// 自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调，默认 false
        /// </summary>
        [JsonPropertyName("auto_color")]
        [JsonProperty("auto_color")]
        public bool AutoColor { get; set; }

        /// <summary>
        /// auto_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"} 十进制表示
        /// </summary>
        [JsonPropertyName("line_color")]
        [JsonProperty("line_color")]
        public LineColorModel LineColor { get; set; }

        /// <summary>
        /// 是否需要透明底色，为 true 时，生成透明底色的小程序
        /// </summary>
        [JsonPropertyName("is_hyaline")]
        [JsonProperty("is_hyaline")]
        public bool IsHyaline { get; set; }

        protected GetUnlimitedACodeRequest()
        {
        }

        /// <summary>
        /// 构造一个新的 <see cref="GetUnlimitedACodeRequest"/> 实例。
        /// </summary>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="checkPage">检查 page 是否存在，为 true 时 page 必须是已经发布的小程序存在的页面（否则报错）；为 false 时允许小程序未发布或者 page 不存在， 但 page 有数量上限（60000个）请勿滥用</param>
        /// <param name="envVersion">要打开的小程序版本。正式版为 release，体验版为 trial，开发版为 develop</param>
        /// <param name="width">二维码的宽度，单位 px，最小 280px，最大 1280px</param>
        /// <param name="autoColor">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调，默认 false</param>
        /// <param name="lineColor">auto_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"} 十进制表示</param>
        /// <param name="isHyaline">是否需要透明底色，为 true 时，生成透明底色的小程序</param>
        public GetUnlimitedACodeRequest(string scene, string page, bool checkPage, string envVersion, short width,
            bool autoColor, LineColorModel lineColor, bool isHyaline)
        {
            Scene = scene;
            Page = page;
            CheckPage = checkPage;
            EnvVersion = envVersion;
            Width = width;
            AutoColor = autoColor;
            LineColor = lineColor;
            IsHyaline = isHyaline;
        }
    }
}