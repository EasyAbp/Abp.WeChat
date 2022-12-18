using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class LoginServiceTests : AbpWeChatMiniProgramTestBase
    {
        public LoginServiceTests()
        {
        }

        [Fact]
        public async Task Should_Get_OpenId_And_SessionKey()
        {
            var loginService = await WeChatServiceFactory.CreateAsync<LoginWeService>();

            var result = await loginService.Code2SessionAsync(
                AbpWeChatMiniProgramTestsConsts.AppId,
                AbpWeChatMiniProgramTestsConsts.AppSecret,
                AbpWeChatMiniProgramTestsConsts.JsCode);
            
            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(0);
            result.ErrorMessage.ShouldBeNull();
            
            result.OpenId.ShouldNotBeEmpty();
            result.SessionKey.ShouldNotBeEmpty();
        }
    }
}