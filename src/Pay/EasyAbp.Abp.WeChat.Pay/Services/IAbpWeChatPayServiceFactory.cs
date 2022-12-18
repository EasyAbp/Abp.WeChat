using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.Services;

public interface IAbpWeChatPayServiceFactory
{
    /// <summary>
    /// 使用本方法实例化一个微信支付服务
    /// </summary>
    /// <param name="mchId">目标微信应用的 mchId，如果为空则取 Setting 中的默认值</param>
    /// <typeparam name="TService">任意微信支付服务类型</typeparam>
    /// <returns></returns>
    Task<TService> CreateAsync<TService>([CanBeNull] string mchId = null) where TService : IAbpWeChatPayService;
}