using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public class AbpWeChatPayResolveOptions
    {
        public List<IWeChatPayOptionResolveContributor> ResolveContributors { get; }

        public AbpWeChatPayResolveOptions()
        {
            ResolveContributors = new List<IWeChatPayOptionResolveContributor>
            {
                new ConfigurationOptionResolveContributor()
            };
        }
    }
}