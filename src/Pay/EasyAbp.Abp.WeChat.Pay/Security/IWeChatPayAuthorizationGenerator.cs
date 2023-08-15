using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.Security;

/// <summary>
/// 用于生成微信支付 V3 接口的签名值，主要用于构建 Authorization 请求头。
/// </summary>
public interface IWeChatPayAuthorizationGenerator
{
    /// <summary>
    /// 生成微信支付 V3 接口的 Authorization 请求头。
    /// </summary>
    /// <param name="method">HTTP 请求方式，具体值请参考 <see cref="HttpMethod"/> 的定义。</param>
    /// <param name="url">需要请求的微信支付 V3 接口地址。</param>
    /// <param name="body">
    /// 请求报文主体，当请求方式为 <see cref="HttpMethod.Get"/> 时报文主体为空。<br/>
    /// 请求方式为 <see cref="HttpMethod.Post"/> 或 <see cref="HttpMethod.Put"/> 时为实际发送的 JSON 报文。<br/>
    /// 如果为某些图片上传 API，请求方式为 meta 对应的 JSON 报文。<br/>
    /// </param>
    /// <param name="mchId">商户号。</param>
    /// <returns>构建完成的 Authorization 请求头。</returns>
    Task<string> GenerateAuthorizationAsync(HttpMethod method, string url, string body, [CanBeNull] string mchId = null);
}