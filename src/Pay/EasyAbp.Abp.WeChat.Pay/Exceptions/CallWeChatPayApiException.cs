using System;
using System.Runtime.Serialization;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;

namespace EasyAbp.Abp.WeChat.Pay.Exceptions
{
    [Serializable]
    public class CallWeChatPayApiException : AbpException, IHasErrorCode, IHasErrorDetails
    {
        public string Code { get; set; }

        public string Details { get; set; }

        public CallWeChatPayApiException()
        {
        }

        public CallWeChatPayApiException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public CallWeChatPayApiException(string message) : base(message)
        {
        }
    }
}