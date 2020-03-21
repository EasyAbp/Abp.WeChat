using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}