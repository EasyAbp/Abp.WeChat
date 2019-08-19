using System.Threading.Tasks;

namespace Zony.Abp.WeiXin.Official
{
    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessToken();
    }
}