using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Security;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Security;

public class CertificatesManagerTests : AbpWeChatPayTestBase
{
    private readonly ICertificatesManager _certificatesManager;

    public CertificatesManagerTests()
    {
        _certificatesManager = GetRequiredService<ICertificatesManager>();
    }

    [Fact]
    public async Task GetCertificateAsync_Test()
    {
        var certificate = await _certificatesManager.GetCertificateAsync(AbpWeChatPayOptions.MchId);

        certificate.ShouldNotBeNull();
        certificate.MchId.ShouldBe(AbpWeChatPayTestConsts.MchId);
        certificate.CertificateHashCode.ShouldNotBeNull();
        certificate.X509Certificate.ShouldNotBeNull();
        certificate.X509Certificate.HasPrivateKey.ShouldBeTrue();
    }
}