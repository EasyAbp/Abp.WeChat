using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform.Fakes;

public class FakeUnauthorizedWeChatThirdPartyPlatformAuthEventHandler : IWeChatThirdPartyPlatformAuthEventHandler,
    ITransientDependency
{
    public string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.Unauthorized;

    public Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model)
    {
        return Task.FromResult(new WeChatRequestHandlingResult(true));
    }
}