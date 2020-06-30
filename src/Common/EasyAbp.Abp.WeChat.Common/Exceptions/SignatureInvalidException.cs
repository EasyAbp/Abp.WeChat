using System;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;

namespace EasyAbp.Abp.WeChat.Common.Exceptions
{
    [Serializable]
    public class SignatureInvalidException : AbpException
    {
        public SignatureInvalidException()
        {
        }
        
        public SignatureInvalidException(string message) : base(message)
        {
        }
    }
}