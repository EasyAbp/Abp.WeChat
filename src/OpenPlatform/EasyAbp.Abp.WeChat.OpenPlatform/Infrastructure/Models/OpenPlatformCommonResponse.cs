using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;

public class OpenPlatformCommonResponse : IOpenPlatformResponse
{
    [JsonProperty("errmsg")] 
    public string ErrorMessage { get; set; }

    [JsonProperty("errcode")] 
    public int ErrorCode { get; set; }
}