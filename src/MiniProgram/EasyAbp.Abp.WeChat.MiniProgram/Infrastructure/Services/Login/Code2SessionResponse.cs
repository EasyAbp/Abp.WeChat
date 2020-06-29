using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Services.Login
{
    public class Code2SessionResponse : IMiniProgramResponse
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }
        
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}