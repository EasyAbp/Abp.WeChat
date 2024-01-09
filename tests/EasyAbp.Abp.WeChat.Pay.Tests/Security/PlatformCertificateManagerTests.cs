using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Security;

public class PlatformCertificateManagerTests : AbpWeChatPayTestBase
{
    private readonly IPlatformCertificateManager _platformCertificateManager;

    public PlatformCertificateManagerTests()
    {
        _platformCertificateManager = GetRequiredService<IPlatformCertificateManager>();
    }

    [Fact]
    public async Task GetPlatformCertificateAsync_Test()
    {
        // Arrange & Act
        var certificate = await _platformCertificateManager.GetPlatformCertificateAsync(AbpWeChatPayTestConsts.MchId,
            AbpWeChatPayTestConsts.SerialNo);

        // Assert
        certificate.ShouldNotBeNull();
        certificate.SerialNo.ShouldNotBeNull();
    }
}