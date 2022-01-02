using System.Runtime.Serialization;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;

namespace EasyAbp.Abp.WeChat.Official.Services.MediaManagement.Exceptions
{
    /// <summary>
    /// 当上传的文件类型不支持时抛出的异常。
    /// </summary>
    public class UnSupportMediaFileTypeException : AbpException, IHasErrorDetails
    {
        public string Details { get; }

        /// <summary>
        /// 构造一个新的 <see cref="UnSupportMediaFileTypeException"/> 对象。
        /// </summary>
        /// <param name="message">错误消息内容。</param>
        /// <param name="details">具体的错误信息。</param>
        public UnSupportMediaFileTypeException(string message, string details) : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// 构造一个新的 <see cref="UnSupportMediaFileTypeException"/> 对象。
        /// </summary>
        /// <param name="serializationInfo">序列化信息。</param>
        /// <param name="context">上下文实例。</param>
        public UnSupportMediaFileTypeException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}