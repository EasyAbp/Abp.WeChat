using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform.Fakes;

public class FakeEventWeChatThirdPartyPlatformAppEventHandler :
    WeChatThirdPartyPlatformAppEventHandlerBase<FakeEventWeChatThirdPartyPlatformAppEventHandler>,
    ITransientDependency
{
    public override string MsgType => "event";
    public override int Priority => 0;

    public override Task<AppEventHandlingResult> HandleAsync(string componentAppId, string authorizerAppId,
        WeChatAppEventModel model)
    {
        return Task.FromResult(new AppEventHandlingResult(true));
    }
}