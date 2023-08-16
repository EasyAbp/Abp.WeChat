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
    public Task<GetWeChatPayCertificatesResponse> GetWeChatPayCertificatesAsync()
    {
        return ApiRequester.RequestAsync<GetWeChatPayCertificatesResponse>(HttpMethod.Get, CertificatesUrl);
    }

    /// <summary>
    /// 获取最新的可用平台证书，方法将会按照证书的生效时间进行排序，返回最新的证书。
    /// </summary>
    public async Task<GetWeChatPayCertificatesResponse.CertificateObject> GetNewestCertificateAsync()
    {
        var certificates = await GetWeChatPayCertificatesAsync();
        return certificates.Data.OrderByDescending(x => x.EffectiveTime).FirstOrDefault();
    }
}