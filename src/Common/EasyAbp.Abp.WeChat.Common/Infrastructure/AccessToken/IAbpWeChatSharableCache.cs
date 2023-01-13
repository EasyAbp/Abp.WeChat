using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;

public interface IAbpWeChatSharableCache
{
    Task<string> GetOrNullAsync([NotNull] string key);

    Task SetAsync([NotNull] string key, [CanBeNull] string value, DistributedCacheEntryOptions options);
}