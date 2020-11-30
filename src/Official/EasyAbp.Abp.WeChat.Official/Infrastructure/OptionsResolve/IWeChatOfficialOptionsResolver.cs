using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptionsResolver
    {
        /// <summary>
        /// 解析微信公众号相关配置。
        /// </summary>
        [Obsolete("Please use asynchronous method.")]
        IWeChatOfficialOptions Resolve();

        /// <summary>
        /// 解析微信公众号相关配置。
        /// </summary>
        ValueTask<IWeChatOfficialOptions> ResolveAsync();
    }
}