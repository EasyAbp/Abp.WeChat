using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.Login
{
    public class Code2SessionResponse : IMiniProgramResponse
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }
        
        [JsonPropertyName("openid")]
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        
        [JsonPropertyName("session_key")]
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        
        [JsonPropertyName("unionid")]
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}