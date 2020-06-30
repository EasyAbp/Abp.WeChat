using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public interface IAccessTokenAccessor
    {
        Task<string> GetAccessTokenAsync();
    }
}