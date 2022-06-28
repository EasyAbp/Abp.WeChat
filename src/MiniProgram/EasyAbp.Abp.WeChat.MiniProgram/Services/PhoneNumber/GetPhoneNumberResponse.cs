using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber
{
    public class GetPhoneNumberResponse : IMiniProgramResponse
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        [JsonProperty("phone_info")]
        public PhoneInfo PhoneInfo { get; set; }
    }

    public class PhoneInfo
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("purePhoneNumber")]
        public string PurePhoneNumber { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("watermark")]
        public WaterMark WaterMark { get; set; }
    }

    public class WaterMark
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }
    }
}
