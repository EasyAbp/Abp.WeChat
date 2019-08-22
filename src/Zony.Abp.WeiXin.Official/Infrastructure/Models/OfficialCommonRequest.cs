using Newtonsoft.Json;

namespace Zony.Abp.WeiXin.Official.Infrastructure.Models
{
    public abstract class OfficialCommonRequest : IOfficialRequest
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}