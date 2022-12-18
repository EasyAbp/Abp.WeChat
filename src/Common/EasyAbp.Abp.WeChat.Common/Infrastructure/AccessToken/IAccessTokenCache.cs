using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;

public interface IAccessTokenCache
{
    Task<string> GetOrNullAsync([NotNull] string key);

    Task SetAsync([NotNull] string key, [CanBeNull] string value);
}