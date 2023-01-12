using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Request
{
    public class UploadTempMediaRequest : OfficialCommonRequest
    {
        /// <summary>
        /// 需要上传的临时素材文件类型，具体类型请参考 <see cref="MediaType"/> 的定义。
        /// </summary>
        [JsonPropertyName("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 构造一个新的 <see cref="UploadTempMediaRequest"/> 对象。
        /// </summary>
        /// <param name="type">需要上传的临时素材文件类型，具体类型请参考 <see cref="MediaType"/> 的定义。</param>
        public UploadTempMediaRequest(string type)
        {
            Type = type;
        }
    }
}