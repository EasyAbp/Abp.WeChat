using System.Threading.Tasks;

namespace Zony.Abp.WeChat.Official.Infrastructure
{
    public interface IJsTicketAccessor
    {
        Task<string> GetTicketJsonAsync();

        Task<string> GetTicketAsync();
    }
}