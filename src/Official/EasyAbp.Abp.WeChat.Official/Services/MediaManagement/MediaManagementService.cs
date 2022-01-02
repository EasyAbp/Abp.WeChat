using System.IO;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Request;
using EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Response;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement
{
    /// <summary>
    /// 素材管理相关服务接口定义。
    /// </summary>
    public class MediaManagementService : CommonService
    {
        protected const string UploadTempMediaUrl = "https://api.weixin.qq.com/cgi-bin/media/upload?";

        /// <summary>
        /// 上传并新增临时素材。<br/>
        /// 注意:<br/>
        /// 1. 临时素材 media_id 是可复用的。<br/>
        /// 2. 媒体文件在微信后台保存时间为 3 天，即 3 天后 media_id 失效。<br/>
        /// 3. 上传临时素材的格式、大小限制与公众平台官网一致。
        /// </summary>
        /// <param name="fileData">需要上传的素材数据。</param>
        /// <param name="type">素材的类型，参考 <see cref="MediaType"/> 的定义。</param>
        public Task<UploadedTempMediaResponse> UploadTempMediaAsync(Stream fileData, string type)
        {
            return WeChatOfficialApiRequester.RequestFromDataAsync<UploadedTempMediaResponse>(UploadTempMediaUrl,
                null,
                new UploadTempMediaRequest(type));
        }
    }
}