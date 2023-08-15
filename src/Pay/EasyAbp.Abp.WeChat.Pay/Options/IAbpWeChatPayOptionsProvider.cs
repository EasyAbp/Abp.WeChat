using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.Options;

/// <summary>
/// 微信支付配置提供器，用于获取微信支付的相关配置。
/// </summary>
/// <remarks>
/// 缺省情况下，会直接从 Settings 中获取配置，如果 Options 中不存在，则会从 Options 中获取配置。
/// </remarks>
public interface IAbpWeChatPayOptionsProvider
{
    /// <summary>
    /// 根据商户号获取微信支付的相关配置。
    /// </summary>
    /// <param name="mchId">微信支付的商户号。</param>
    /// <returns>获取到的对应商户号配置信息。</returns>
    Task<AbpWeChatPayOptions> GetAsync([CanBeNull] string mchId);
}