using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;

public interface IOpenPlatformResponse
{
    [JsonProperty("errmsg")] string ErrorMessage { get; set; }

    [JsonProperty("errcode")] int ErrorCode { get; set; }
}