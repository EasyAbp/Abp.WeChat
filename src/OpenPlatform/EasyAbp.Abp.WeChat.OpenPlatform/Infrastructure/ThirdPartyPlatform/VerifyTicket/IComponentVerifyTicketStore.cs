using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;

public interface IComponentVerifyTicketStore
{
    Task<string> GetOrNullAsync(string componentAppId);

    Task SetAsync(string componentAppId, string componentVerifyTicket);
}