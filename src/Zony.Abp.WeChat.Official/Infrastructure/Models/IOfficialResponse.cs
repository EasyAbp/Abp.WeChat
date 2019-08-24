using Newtonsoft.Json;

namespace Zony.Abp.WeChat.Official.Infrastructure.Models
{
    public interface IOfficialResponse
    {
        [JsonProperty("errmsg")]
        string ErrorMessage { get; set; }

        [JsonProperty("errcode")]
        int ErrorCode { get; set; }
    }
}