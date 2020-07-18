using System.Collections.Generic;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public class AbpWeChatMiniProgramResolveOptions
    {
        public List<IWeChatMiniProgramOptionsResolveContributor> Contributors { get; }

        public AbpWeChatMiniProgramResolveOptions()
        {
            Contributors = new List<IWeChatMiniProgramOptionsResolveContributor>();
        }
    }
}