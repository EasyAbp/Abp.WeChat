using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Official.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Tests.EventHandling.Fakes;

public class FakeTextWeChatOfficialAppEventHandler : IWeChatOfficialAppEventHandler,
    ITransientDependency
{
    public string MsgType => "text";
    public int Priority => 0;

    public Task<AppEventHandlingResult> HandleAsync(string aAppId, WeChatAppEventModel model)
    {
        return Task.FromResult(new AppEventHandlingResult(true));
    }
}