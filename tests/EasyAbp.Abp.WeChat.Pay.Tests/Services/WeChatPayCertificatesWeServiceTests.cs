using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.OtherServices;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services;

public class WeChatPayCertificatesWeServiceTests : AbpWeChatPayTestBase
{
    private readonly IAbpWeChatPayServiceFactory _abpWeChatPayServiceFactory;

    public WeChatPayCertificatesWeServiceTests()
    {
        _abpWeChatPayServiceFactory = GetRequiredService<IAbpWeChatPayServiceFactory>();
    }

    [Fact]
    public async Task GetWeChatPayCertificatesAsync_Test()
    {
        // Arrange
        var weChatPayCertificatesWeService = await _abpWeChatPayServiceFactory.CreateAsync<WeChatPayCertificatesWeService>();

        // Act
        var response = await weChatPayCertificatesWeService.GetWeChatPayCertificatesAsync();

        // Assert
        response.ShouldNotBeNull();
        response.Code.ShouldBeNullOrEmpty();
        response.Data.Count().ShouldBeGreaterThan(0);
    }

    public async Task DecryptWeChatPayCertificateAsync_Test()
    {
        // Arrange
        var weChatPayCertificatesWeService = await _abpWeChatPayServiceFactory.CreateAsync<WeChatPayCertificatesWeService>();
        var weChatPayCertificate = (await weChatPayCertificatesWeService.GetWeChatPayCertificatesAsync())
            .Data.MaxBy(d => d.EffectiveTime);

        // Act
        weChatPayCertificate.ShouldNotBeNull();
        // WeChatPaySecurityUtility.AesGcmDecrypt(AbpWeChatPayOptions.ApiKey,)

        // Assert
    }
}