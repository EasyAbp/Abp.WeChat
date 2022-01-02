using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Response
{
    public class UploadedTempMediaResponse : OfficialCommonResponse
    {
        /// <summary>
        /// 媒体文件的类型，参考 <see cref="MediaType"/> 的定义。
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// 媒体文件上传成功以后，获取该文件的临时唯一标识。
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaId { get; private set; }

        /// <summary>
        /// 媒体文件上传的时间戳。
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; private set; }
    }
}