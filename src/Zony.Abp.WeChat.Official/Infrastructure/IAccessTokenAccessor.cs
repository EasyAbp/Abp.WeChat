using System.Threading.Tasks;

namespace Zony.Abp.WeChat.Official.Infrastructure
{
    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}