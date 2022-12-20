using System.Net.Http;

namespace EasyAbp.Abp.WeChat.MiniProgram.Models
{
    public interface IMiniProgramRequest
    {
        StringContent ToStringContent();
    }
}