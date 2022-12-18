using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Options;

public interface IAbpWeChatOptionsProvider<TOptions> : IAbpWeChatOptionsProvider where TOptions : IAbpWeChatOptions
{
    Task<TOptions> GetAsync([CanBeNull] string appId);
}

public interface IAbpWeChatOptionsProvider
{
    Task<IAbpWeChatOptions> GetBasicAsync([CanBeNull] string appId);
}