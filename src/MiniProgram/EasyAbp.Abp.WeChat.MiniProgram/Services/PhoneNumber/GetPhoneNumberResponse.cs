using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber
{
    public class GetPhoneNumberResponse : IMiniProgramResponse
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        [JsonPropertyName("phone_info")]
        [JsonProperty("phone_info")]
        public PhoneInfo PhoneInfo { get; set; }
    }

    public class PhoneInfo
    {
        [JsonPropertyName("phoneNumber")]
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("purePhoneNumber")]
        [JsonProperty("purePhoneNumber")]
        public string PurePhoneNumber { get; set; }

        [JsonPropertyName("countryCode")]
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("watermark")]
        [JsonProperty("watermark")]
        public WaterMark WaterMark { get; set; }
    }

    public class WaterMark
    {
        [JsonPropertyName("appid")]
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonProperty("timestamp")]
        public int TimeStamp { get; set; }
    }
}
