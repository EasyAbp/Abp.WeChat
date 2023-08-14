using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.ACode
{
    /// <summary>
    /// 小程序码服务。
    /// </summary>
    public class ACodeWeService : MiniProgramAbpWeChatServiceBase
    {
        public ACodeWeService(AbpWeChatMiniProgramOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(
            options, lazyServiceProvider)
        {
        }

        /// <summary>
        /// 获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="checkPage">检查 page 是否存在，为 true 时 page 必须是已经发布的小程序存在的页面（否则报错）；为 false 时允许小程序未发布或者 page 不存在， 但 page 有数量上限（60000个）请勿滥用</param>
        /// <param name="envVersion">要打开的小程序版本。正式版为 release，体验版为 trial，开发版为 develop</param>
        /// <param name="width">二维码的宽度，单位 px，最小 280px，最大 1280px</param>
        /// <param name="autoColor">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调，默认 false</param>
        /// <param name="lineColor">auto_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"} 十进制表示</param>
        /// <param name="isHyaline">是否需要透明底色，为 true 时，生成透明底色的小程序</param>
        public virtual Task<GetUnlimitedACodeResponse> GetUnlimitedACodeAsync(string scene, string page = null,
            bool checkPage = true, string envVersion = "release", short width = 430, bool autoColor = false,
            LineColorModel lineColor = null, bool isHyaline = false)
        {
            const string targetUrl = "https://api.weixin.qq.com/wxa/getwxacodeunlimit";

            var request = new GetUnlimitedACodeRequest(
                scene, page, checkPage, envVersion, width, autoColor, lineColor, isHyaline);

            return ApiRequester.RequestGetBinaryDataAsync<GetUnlimitedACodeResponse>(
                targetUrl, HttpMethod.Post, request, Options);
        }
    }
}