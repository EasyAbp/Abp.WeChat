using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Models
{
    public interface IMiniProgramResponse
    {
        [JsonProperty("errmsg")] string ErrorMessage { get; set; }

        [JsonProperty("errcode")] int ErrorCode { get; set; }
    }
}