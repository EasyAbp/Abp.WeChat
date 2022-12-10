using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public interface IWeChatOpenPlatformOptionsResolveContributor
{
    string Name { get; }

    [Obsolete("Please use asynchronous method.")]
    void Resolve(WeChatOpenPlatformResolveContext context);

    ValueTask ResolveAsync(WeChatOpenPlatformResolveContext context);
}