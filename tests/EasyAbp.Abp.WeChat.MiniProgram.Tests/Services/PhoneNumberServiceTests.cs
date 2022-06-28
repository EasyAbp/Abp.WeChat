using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class PhoneNumberServiceTests : AbpWeChatMiniProgramTestBase
    {
        private readonly PhoneNumberService _phoneNumberService;

        public PhoneNumberServiceTests()
        {
            _phoneNumberService = GetRequiredService<PhoneNumberService>();
        }

        [Fact]
        public async Task Should_Get_Error()
        {
            var result = await _phoneNumberService.GetPhoneNumberAsync("test");

            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldNotBeNull();
            result.ErrorCode.ShouldNotBe(0);
            result.PhoneInfo.ShouldBeNull();
        }
    }
}
