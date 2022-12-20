using System.Net.Http;

namespace EasyAbp.Abp.WeChat.Official.Models
{
    public interface IOfficialRequest
    {
        StringContent ToStringContent();
    }
}