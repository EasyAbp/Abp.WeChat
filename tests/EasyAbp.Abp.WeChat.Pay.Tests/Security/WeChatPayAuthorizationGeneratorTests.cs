using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Security;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Security;

public class WeChatPayAuthorizationGeneratorTests : AbpWeChatPayTestBase
{
    private readonly IWeChatPayAuthorizationGenerator _weChatPayAuthorizationGenerator;

    public WeChatPayAuthorizationGeneratorTests()
    {
        _weChatPayAuthorizationGenerator = GetRequiredService<IWeChatPayAuthorizationGenerator>();
    }

    [Fact]
    public async Task GenerateAuthorizationAsync_Test()
    {
        // Arrange & Act
        var authorization = await _weChatPayAuthorizationGenerator.GenerateAuthorizationAsync(
            HttpMethod.Post, "https://api.mch.weixin.qq.com/v3/certificates", null);

        // Assert
        authorization.ShouldNotBeNull();
        authorization.ShouldNotBeNullOrEmpty();
    }

    public async Task ValidateAuthorizationAsync_Test()
    {
        // Arrange
        
    }
}