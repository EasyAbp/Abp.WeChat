using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.ACode;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class ACodeService_Tests : AbpWeChatMiniProgramTestBase
    {
        private readonly ACodeService _aCodeService;

        public ACodeService_Tests()
        {
            _aCodeService = GetRequiredService<ACodeService>();
        }

        [Fact]
        public async Task Should_Get_Unlimited_ACode()
        {
            var result = await _aCodeService.GetUnlimitedACodeAsync("test");
            
            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldBeNull();
            result.ErrorCode.ShouldBe(0);
            result.BinaryData.ShouldNotBeEmpty();
        }
    }
}