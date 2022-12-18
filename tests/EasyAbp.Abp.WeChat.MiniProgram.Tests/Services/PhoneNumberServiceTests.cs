using EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class PhoneNumberServiceTests : AbpWeChatMiniProgramTestBase
    {
        public PhoneNumberServiceTests()
        {
        }

        [Fact]
        public async Task Should_Get_Error()
        {
            var phoneNumberService = await WeChatServiceFactory.CreateAsync<PhoneNumberWeService>();

            var result = await phoneNumberService.GetPhoneNumberAsync("test");

            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldNotBeNull();
            result.ErrorCode.ShouldNotBe(0);
            result.PhoneInfo.ShouldBeNull();
        }
    }
}
