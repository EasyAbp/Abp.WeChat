using System.Threading.Tasks;
using Volo.Abp.Caching;
using Zony.Abp.WeiXin.Common;

namespace Zony.Abp.WeiXin.Official
{
    public class CacheTokenAccessor : IAccessTokenAccessor
    {
        private readonly IDistributedCache<string> _distributedCache;

        public CacheTokenAccessor(IDistributedCache<string> distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task<string> GetAccessToken()
        {
            return _distributedCache.GetAsync("CurrentAccessToken");
        }
    }
}