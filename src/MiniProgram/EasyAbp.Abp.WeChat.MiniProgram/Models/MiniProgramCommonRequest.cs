using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Models
{
    public abstract class MiniProgramCommonRequest : IMiniProgramRequest
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
}