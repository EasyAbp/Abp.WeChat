using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;

public interface IWeChatThirdPartyPlatformOptionsResolveContributor
{
    string Name { get; }

    [Obsolete("Please use asynchronous method.")]
    void Resolve(WeChatThirdPartyPlatformOptionsResolveContext context);

    ValueTask ResolveAsync(WeChatThirdPartyPlatformOptionsResolveContext context);
}