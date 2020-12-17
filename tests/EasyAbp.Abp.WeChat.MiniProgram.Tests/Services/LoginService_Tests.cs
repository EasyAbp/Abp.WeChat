using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class LoginService_Tests : AbpWeChatMiniProgramTestBase
    {
        private readonly LoginService _loginService;

        public LoginService_Tests()
        {
            _loginService = GetRequiredService<LoginService>();
        }

        [Fact]
        public async Task Should_Get_OpenId_And_SessionKey()
        {
            var result = await _loginService.Code2SessionAsync(
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