﻿using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public class AbpWeChatOfficialResolveOptions
    {
        [NotNull] public List<IWeChatOfficialOptionsResolveContributor> WeChatOfficialOptionsResolveContributors { get; }

        public AbpWeChatOfficialResolveOptions()
        {
            WeChatOfficialOptionsResolveContributors = new List<IWeChatOfficialOptionsResolveContributor>
            {
                new ConfigurationOptionsResolveContributor()
            };
        }
    }
}