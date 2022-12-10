using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

public class CreateUserTagResponse : OpenPlatformCommonResponse
{
    /// <summary>
    /// 创建成功的标签。
    /// </summary>
    [JsonProperty("tag")]
    public UserTagDefinition Tag { get; set; }
}