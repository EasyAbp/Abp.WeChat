using Volo.Abp;
using Volo.Abp.ExceptionHandling;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Exceptions
{
    /// <summary>
    /// 当媒体文件校验失败之后，会抛出本异常。
    /// </summary>
    public class ValidateMediaTypeException : AbpException, IHasErrorDetails
    {
        public string Details { get; }

        /// <summary>
        /// 构造一个新的 <see cref="ValidateMediaTypeException"/> 对象。
        /// </summary>
        /// <param name="message">错误消息内容。</param>
        /// <param name="details">具体的错误信息。</param>
        public ValidateMediaTypeException(string details, string message) : base(message)
        {
            Details = details;
        }
    }
}