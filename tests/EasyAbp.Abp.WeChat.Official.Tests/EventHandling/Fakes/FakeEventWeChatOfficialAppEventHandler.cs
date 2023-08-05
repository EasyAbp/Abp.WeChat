using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Official.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Tests.EventHandling.Fakes;

public class FakeEventWeChatOfficialAppEventHandler :
    WeChatOfficialAppEventHandlerBase<FakeEventWeChatOfficialAppEventHandler>,
    ITransientDependency
{
    public override string MsgType => "event";
    public override int Priority => 0;

    public override Task<AppEventHandlingResult> HandleAsync(string appId, WeChatAppEventModel model)
    {
        return Task.FromResult(new AppEventHandlingResult(true));
    }
}