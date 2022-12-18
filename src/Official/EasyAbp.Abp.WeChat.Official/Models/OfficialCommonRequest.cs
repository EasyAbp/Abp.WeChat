using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Models
{
    public abstract class OfficialCommonRequest : IOfficialRequest
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }
    }
}