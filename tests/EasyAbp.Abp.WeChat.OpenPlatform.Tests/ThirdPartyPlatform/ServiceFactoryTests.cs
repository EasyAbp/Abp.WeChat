using System.Reflection;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class ServiceFactoryTests : AbpWeChatOpenPlatformTestBase
{
    [Fact]
    public async Task Should_Create_Service_Instance()
    {
        var apiService = await WeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>();

        apiService.ShouldNotBeNull();

        var options =
            (AbpWeChatThirdPartyPlatformOptions)apiService.GetType()
                .GetProperty("Options", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(apiService)!;

        options.AppId.ShouldBe(AbpWeChatOpenPlatformTestsConsts.AppId);
        options.AppSecret.ShouldBe(AbpWeChatOpenPlatformTestsConsts.AppSecret);
        options.Token.ShouldBe(AbpWeChatOpenPlatformTestsConsts.Token);
        options.EncodingAesKey.ShouldBe(AbpWeChatOpenPlatformTestsConsts.EncodingAesKey);
    }
}