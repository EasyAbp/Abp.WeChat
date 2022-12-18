using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Models
{
    public interface IOfficialResponse
    {
        [JsonProperty("errmsg")] string ErrorMessage { get; set; }

        [JsonProperty("errcode")] int ErrorCode { get; set; }
    }
}