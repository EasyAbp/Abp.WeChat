using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Official.JsTickets
{
    public interface IJsTicketProvider
    {
        Task<string> GetTicketJsonAsync(string appId, string appSecret);

        Task<string> GetTicketAsync(string appId, string appSecret);
    }
}