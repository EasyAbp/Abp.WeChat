using System.Threading.Tasks;

namespace Zony.Abp.WeiXin.Official.Infrastructure
{
    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}