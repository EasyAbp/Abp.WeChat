using System;
using Volo.Abp;

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