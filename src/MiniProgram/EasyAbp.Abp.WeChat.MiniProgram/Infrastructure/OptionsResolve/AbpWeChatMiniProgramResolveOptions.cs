using System.Collections.Generic;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class AbpWeChatMiniProgramResolveOptions
    {
        [NotNull] public List<IWeChatMiniProgramOptionsResolveContributor> WeChatMiniProgramOptionsResolveContributors { get; }

        public AbpWeChatMiniProgramResolveOptions()
        {
            WeChatMiniProgramOptionsResolveContributors = new List<IWeChatMiniProgramOptionsResolveContributor>
            {
                new ConfigurationOptionsResolveContributor()
            };
        }
    }
}