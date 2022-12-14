using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;

public class AbpWeChatThirdPartyPlatformResolvingOptions
{
    [NotNull] public List<IWeChatThirdPartyPlatformOptionsResolvingContributor> WeChatThirdPartyPlatformOptionsResolveContributors { get; }

    public AbpWeChatThirdPartyPlatformResolvingOptions()
    {
        WeChatThirdPartyPlatformOptionsResolveContributors = new List<IWeChatThirdPartyPlatformOptionsResolvingContributor>
        {
            new ConfigurationOptionsResolvingContributor()
        };
    }
}