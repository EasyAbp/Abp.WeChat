using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;

public interface IWeChatThirdPartyPlatformOptionsResolver
{
    /// <summary>
    /// 解析微信公众号相关配置。
    /// </summary>
    [Obsolete("Please use asynchronous method.")]
    IWeChatThirdPartyPlatformOptions Resolve();

    /// <summary>
    /// 解析微信公众号相关配置。
    /// </summary>
    ValueTask<IWeChatThirdPartyPlatformOptions> ResolveAsync();
}