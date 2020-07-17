using System.Net.Http;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.ACode
{
    /// <summary>
    /// 小程序码服务。
    /// </summary>
    public class ACodeService : CommonService
    {
        /// <summary>
        /// 获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">二维码的宽度，单位 px，最小 280px，最大 1280px</param>
        /// <param name="autoColor">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调，默认 false</param>
        /// <param name="lineColor">auto_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"} 十进制表示</param>
        /// <param name="isHyaline">是否需要透明底色，为 true 时，生成透明底色的小程序</param>
        public Task<GetUnlimitedACodeResponse> GetUnlimitedACodeAsync(string scene, string page = null,
            short width = 430, bool autoColor = false, LineColorModel lineColor = null, bool isHyaline = false)
        {
            const string targetUrl = "https://api.weixin.qq.com/wxa/getwxacodeunlimit";

            var request = new GetUnlimitedACodeRequest(scene, page, width, autoColor, lineColor, isHyaline);

            return WeChatMiniProgramApiRequester.RequestGetBinaryDataAsync<GetUnlimitedACodeResponse>(targetUrl,
                HttpMethod.Post, request);
        }
    }
}