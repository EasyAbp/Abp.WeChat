using System.IO;
using EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Exceptions;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement
{
    /// <summary>
    /// 上传素材的时候，支持的媒体文件类型。
    /// </summary>
    public static class MediaType
    {
        /// <summary>
        /// 图片，大小限制 10M，支持 PNG\JPEG\JPG\GIF 格式。
        /// </summary>
        public const string Image = "image";

        public const int MaxImageSize = 1024 * 1024 * 10;

        /// <summary>
        /// 语音，2M，播放长度不超过 60s，支持AMR\MP3格式。
        /// </summary>
        public const string Voice = "voice";

        public const int MaxVoiceSize = 1024 * 1024 * 2;

        /// <summary>
        /// 视频，10MB，支持 MP4 格式。
        /// </summary>
        public const string Video = "video";

        public const int MaxVideoSize = 1024 * 1024 * 10;

        /// <summary>
        /// 缩略图，64KB，支持JPG格式。
        /// </summary>
        public const string Thumb = "thumb";

        public const int MaxThumbSize = 1024 * 64;

        public static bool IsValidMediaFile(Stream fileContent, string fileType)
        {
            switch (fileType)
            {
                case Image:
                    if (fileContent.Length > MaxImageSize)
                    {
                        return false;
                    }

                    break;
                case Voice:
                    if (fileContent.Length > MaxVoiceSize)
                    {
                        return false;
                    }

                    break;
                case Video:
                    if (fileContent.Length > MaxVideoSize)
                    {
                        return false;
                    }

                    break;
                case Thumb:
                    if (fileContent.Length > MaxThumbSize)
                    {
                        return false;
                    }

                    break;
                default:
                    throw new UnSupportMediaFileTypeException("不支持的媒体文件类型", $"文件类型: {fileType}");
            }

            return true;
        }
    }
}