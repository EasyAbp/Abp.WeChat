using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Zony.Abp.WeChat.Pay.Services.MicroMerchant;

namespace Zony.Abp.WeChat.Pay.Tests.Services
{
    public class MicroMerchantServiceTests : AbpWeChatPayTestBase
    {
        private readonly MicroMerchantService _service;

        public MicroMerchantServiceTests()
        {
            _service = GetRequiredService<MicroMerchantService>();
        }

        [Fact]
        public async Task Should_Return_Certificate_Serial_Number()
        {
            // Arrange & Act
            var result = await _service.GetCertificateAsync("1540561391");

            // Assert
            result.ShouldNotBeNull();
        }
    }
}