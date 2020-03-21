using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public interface IJsTicketAccessor
    {
        Task<string> GetTicketJsonAsync();

        Task<string> GetTicketAsync();
    }
}