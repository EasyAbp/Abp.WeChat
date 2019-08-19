using System.Threading.Tasks;
using Volo.Abp.Caching;
using Zony.Abp.WeiXin.Common;

namespace Zony.Abp.WeiXin.Official
{
    public class CacheAccessTokenAccessor : IAccessTokenAccessor
    {
        private readonly IDistributedCache<string> _distributedCache;

        public CacheAccessTokenAccessor(IDistributedCache<string> distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task<string> GetAccessToken()
        {
            return _distributedCache.GetAsync("CurrentAccessToken");
        }
    }
}