using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;

public class AbpWeChatThirdPartyPlatformResolveOptions
{
    [NotNull] public List<IWeChatThirdPartyPlatformOptionsResolveContributor> WeChatThirdPartyPlatformOptionsResolveContributors { get; }

    public AbpWeChatThirdPartyPlatformResolveOptions()
    {
        WeChatThirdPartyPlatformOptionsResolveContributors = new List<IWeChatThirdPartyPlatformOptionsResolveContributor>
        {
            new ConfigurationOptionsResolveContributor()
        };
    }
}