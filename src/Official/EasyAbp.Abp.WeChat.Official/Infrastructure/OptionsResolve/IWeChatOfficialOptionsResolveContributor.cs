using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptionsResolveContributor
    {
        string Name { get; }

        [Obsolete("Please use asynchronous method.")]
        void Resolve(WeChatOfficialResolveContext context);

        ValueTask ResolveAsync(WeChatOfficialResolveContext context);
    }
}