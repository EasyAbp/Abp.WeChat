using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices.ParametersModel;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.OtherServices;

/// <summary>
/// 提供平台证书获取的相关服务。
/// </summary>
public class WeChatPayCertificatesWeService : WeChatPayServiceBase
{
    public const string CertificatesUrl = "https://api.mch.weixin.qq.com/v3/certificates";

    public WeChatPayCertificatesWeService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    /// <summary>
    /// 获取商户当前可用的平台证书列表。
    /// </summary>
    public virtual Task<GetPlatformCertificatesResponse> GetPlatformCertificatesAsync()
    {
        return ApiRequester.RequestAsync<GetPlatformCertificatesResponse>(HttpMethod.Get, CertificatesUrl);
    }
}