using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;

public interface IWeChatThirdPartyPlatformOptionsResolvingContributor
{
    string Name { get; }

    [Obsolete("Please use asynchronous method.")]
    void Resolve(WeChatThirdPartyPlatformOptionsResolvingContext context);

    ValueTask ResolveAsync(WeChatThirdPartyPlatformOptionsResolvingContext context);
}