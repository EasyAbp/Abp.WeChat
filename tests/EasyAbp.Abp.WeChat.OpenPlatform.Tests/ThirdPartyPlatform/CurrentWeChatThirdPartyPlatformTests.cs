using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class CurrentWeChatThirdPartyPlatformTests : AbpWeChatOpenPlatformTestBase
{
    private readonly ICurrentWeChatThirdPartyPlatform _currentWeChatThirdPartyPlatform;

    private readonly string _componentAppId1 = "1";
    private readonly string _componentAppId2 = "2";

    public CurrentWeChatThirdPartyPlatformTests()
    {
        _currentWeChatThirdPartyPlatform = ServiceProvider.GetRequiredService<ICurrentWeChatThirdPartyPlatform>();
    }

    [Fact]
    public void ComponentAppId_Should_Be_Null_As_Default()
    {
        _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBeNull();
    }

    [Fact]
    public void Should_Get_Changed_ComponentAppId()
    {
        _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBe(null);

        using (_currentWeChatThirdPartyPlatform.Change(_componentAppId1))
        {
            _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBe(_componentAppId1);

            using (_currentWeChatThirdPartyPlatform.Change(_componentAppId2))
            {
                _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBe(_componentAppId2);
            }

            _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBe(_componentAppId1);
        }

        _currentWeChatThirdPartyPlatform.ComponentAppId.ShouldBeNull();
    }
}