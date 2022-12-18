using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Services;

public interface IAbpWeChatServiceFactory
{
    /// <summary>
    /// 使用本方法实例化一个微信服务
    /// </summary>
    /// <param name="appId">目标微信应用的 appid，如果为空则取 Setting 中的默认值</param>
    /// <typeparam name="TService">任意微信服务类型</typeparam>
    /// <returns></returns>
    Task<TService> CreateAsync<TService>([CanBeNull] string appId = null) where TService : IAbpWeChatService;
}