using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform.Fakes;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class ThirdPartyPlatformEventHandlerResolverTests : AbpWeChatOpenPlatformTestBase
{
    [Fact]
    public async Task Should_Resolve_Handlers()
    {
        var resolver = GetRequiredService<IThirdPartyPlatformEventHandlerResolver>();

        var unauthorizedHandlers =
            await resolver.GetAuthEventHandlersAsync(WeChatThirdPartyPlatformAuthEventInfoTypes.Unauthorized);

        unauthorizedHandlers.Count.ShouldBe(1);
        unauthorizedHandlers.ShouldContain(x =>
            x.GetType() == typeof(FakeUnauthorizedWeChatThirdPartyPlatformAuthEventHandler));

        var textHandlers = await resolver.GetAppEventHandlersAsync("text");

        textHandlers.Count.ShouldBe(2);
        textHandlers.ShouldContain(x =>
            x.GetType() == typeof(ReleaseTestWeChatThirdPartyPlatformAppEventHandler));
        textHandlers.ShouldContain(x =>
            x.GetType() == typeof(FakeTextWeChatThirdPartyPlatformAppEventHandler));

        var eventHandlers = await resolver.GetAppEventHandlersAsync("event");

        eventHandlers.Count.ShouldBe(1);
        eventHandlers.ShouldContain(x =>
            x.GetType() == typeof(FakeEventWeChatThirdPartyPlatformAppEventHandler));
    }
}