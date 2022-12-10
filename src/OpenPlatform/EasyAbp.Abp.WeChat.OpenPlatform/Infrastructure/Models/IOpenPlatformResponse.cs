using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;

public interface IOpenPlatformResponse
{
    [JsonProperty("errmsg")] string ErrorMessage { get; set; }

    [JsonProperty("errcode")] int ErrorCode { get; set; }
}