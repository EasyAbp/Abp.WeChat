using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public class AbpWeChatOpenPlatformResolveOptions
{
    [NotNull] public List<IWeChatOpenPlatformOptionsResolveContributor> WeChatOpenPlatformOptionsResolveContributors { get; }

    public AbpWeChatOpenPlatformResolveOptions()
    {
        WeChatOpenPlatformOptionsResolveContributors = new List<IWeChatOpenPlatformOptionsResolveContributor>
        {
            new ConfigurationOptionsResolveContributor()
        };
    }
}