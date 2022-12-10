using System;
using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;

public interface IWeChatOpenPlatformOptionsResolver
{
    /// <summary>
    /// 解析微信公众号相关配置。
    /// </summary>
    [Obsolete("Please use asynchronous method.")]
    IWeChatOpenPlatformOptions Resolve();

    /// <summary>
    /// 解析微信公众号相关配置。
    /// </summary>
    ValueTask<IWeChatOpenPlatformOptions> ResolveAsync();
}