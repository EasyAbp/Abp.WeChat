using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;

public abstract class OpenPlatformCommonRequest : IOpenPlatformRequest
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
    }
}