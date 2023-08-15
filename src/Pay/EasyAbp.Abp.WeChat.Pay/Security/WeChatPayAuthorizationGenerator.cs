using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using EasyAbp.Abp.WeChat.Pay.Options;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Security;

/// <summary>
/// <see cref="IWeChatPayAuthorizationGenerator"/> 的默认实现。
/// </summary>
public class WeChatPayAuthorizationGenerator : IWeChatPayAuthorizationGenerator, ITransientDependency
{
    /// <summary>
    /// 授权(Authorization)标头的认证类型。
    /// </summary>
    public const string AuthorizationScheme = "WECHATPAY2-SHA256-RSA2048";
    
    private readonly IAbpWeChatPayOptionsProvider _weChatPayOptionsProvider;
    private readonly ICertificatesManager _certificatesManager;

    public WeChatPayAuthorizationGenerator(IAbpWeChatPayOptionsProvider weChatPayOptionsProvider,
        ICertificatesManager certificatesManager)
    {
        _weChatPayOptionsProvider = weChatPayOptionsProvider;
        _certificatesManager = certificatesManager;
    }

    public async Task<string> GenerateAuthorizationAsync(HttpMethod method, string url, string body, [CanBeNull] string mchId = null)
    {
        var options = await _weChatPayOptionsProvider.GetAsync(mchId);
        var timeStamp = DateTimeHelper.GetNowTimeStamp().ToString();
        var nonceStr = RandomStringHelper.GetRandomString();

        var requestModel = new WeChatPayApiRequestModel(method, url, body, timeStamp, nonceStr);
        var pendingSignature = requestModel.GetPendingSignatureString();
        var certificate = await _certificatesManager.GetCertificateAsync(mchId);
        var signString = RsaSign(pendingSignature, certificate);

        return
            $"{AuthorizationScheme} mchid=\"{options.MchId}\",nonce_str=\"{nonceStr}\",timestamp=\"{timeStamp}\",serial_no=\"{certificate.X509Certificate.SerialNumber}\",signature=\"{signString}\"";
    }

    private string RsaSign(string pendingSignature, WeChatPayCertificate certificate)
    {
        var privateKey = certificate.X509Certificate.GetRSAPrivateKey();
        var signDataBytes = privateKey.SignData(Encoding.UTF8.GetBytes(pendingSignature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return Convert.ToBase64String(signDataBytes);
    }
}