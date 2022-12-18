using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.ACode;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class ACodeServiceTests : AbpWeChatMiniProgramTestBase
    {
        public ACodeServiceTests()
        {
        }

        [Fact]
        public async Task Should_Get_Unlimited_ACode()
        {
            var aCodeService = await WeChatServiceFactory.CreateAsync<ACodeWeService>();

            var result = await aCodeService.GetUnlimitedACodeAsync("test");

            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldBeNull();
            result.ErrorCode.ShouldBe(0);
            result.BinaryData.ShouldNotBeEmpty();
        }
    }
}