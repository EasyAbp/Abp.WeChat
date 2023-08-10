using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;

public abstract class OpenPlatformCommonRequest : IOpenPlatformRequest
{
    public virtual StringContent ToStringContent()
    {
        return new StringContent(
            JsonConvert.SerializeObject(this,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            Encoding.UTF8,
            "application/json");
    }
}