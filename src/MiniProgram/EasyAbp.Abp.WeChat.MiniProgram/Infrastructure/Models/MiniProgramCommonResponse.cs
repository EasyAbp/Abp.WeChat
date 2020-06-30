using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models
{
    public class MiniProgramCommonResponse : IMiniProgramResponse
    {
        [JsonProperty("errmsg")] public string ErrorMessage { get; set; }

        [JsonProperty("errcode")] public int ErrorCode { get; set; }
    }
}