using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure;

public interface IAccessTokenAccessor
{
    Task<string> GetAccessTokenAsync();
}