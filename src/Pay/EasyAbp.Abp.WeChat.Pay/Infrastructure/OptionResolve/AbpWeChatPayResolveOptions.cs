using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public class AbpWeChatPayResolveOptions
    {
        public List<IWeChatPayOptionsResolveContributor> Contributors { get; }

        public AbpWeChatPayResolveOptions()
        {
            Contributors = new List<IWeChatPayOptionsResolveContributor>();
        }
    }
}