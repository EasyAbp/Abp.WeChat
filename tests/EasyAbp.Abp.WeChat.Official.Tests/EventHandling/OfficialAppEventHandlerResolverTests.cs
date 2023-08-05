using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.RequestHandling;
using EasyAbp.Abp.WeChat.Official.Tests.EventHandling.Fakes;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.EventHandling;

public class OfficialAppEventHandlerResolverTests : AbpWeChatOfficialTestBase
{
    [Fact]
    public async Task Should_Resolve_Handlers()
    {
        var resolver = GetRequiredService<IWeChatOfficialAppEventHandlerResolver>();

        var textHandlers = await resolver.GetAppEventHandlersAsync("text");

        textHandlers.Count.ShouldBe(1);
        textHandlers.ShouldContain(x =>
            x.GetType() == typeof(FakeTextWeChatOfficialAppEventHandler));

        var eventHandlers = await resolver.GetAppEventHandlersAsync("event");

        eventHandlers.Count.ShouldBe(1);
        eventHandlers.ShouldContain(x =>
            x.GetType() == typeof(FakeEventWeChatOfficialAppEventHandler));
    }
}