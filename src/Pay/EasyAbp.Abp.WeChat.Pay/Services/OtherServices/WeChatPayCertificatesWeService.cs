using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices.ParametersModel;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.OtherServices;

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
    public Task<GetWeChatPayCertificatesResponse> GetWeChatPayCertificatesAsync()
    {
        return ApiRequester.RequestAsync<GetWeChatPayCertificatesResponse>(HttpMethod.Get, CertificatesUrl);
    }
}