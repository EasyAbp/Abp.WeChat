using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EasyAbp.Abp.WeChat.Common.Exceptions;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Signature
{
    public class SignatureChecker : ITransientDependency
    {
        /// <summary>
        /// 校验请求参数是否有效。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timeStamp"></param>
        /// <param name="nonce"></param>
        /// <param name="signature"></param>
        /// <returns>返回 true 说明是有效请求，返回 false 说明是无效请求。</returns>
        public bool Validate(string token, string timeStamp, string nonce, string signature)
        {
            var paraArray = new[] {token, timeStamp, nonce}.OrderBy(x => x).ToArray();
            var paraString = string.Join(string.Empty, paraArray);
            var bytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(paraString));

            var signStrBuilder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                signStrBuilder.Append($"{@byte:x2}");
            }

            return signStrBuilder.ToString().Equals(signature);
        }
        
        /// <summary>
        /// 检验数据签名是否有效。
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="sessionKey"></param>
        /// <param name="signature"></param>
        /// <returns>返回 true 说明是有效数据，返回 false 说明是无效数据。</returns>
        public bool Validate(string rawData, string sessionKey, string signature)
        {
            var paraArray = new[] {rawData, sessionKey}.ToArray();
            var paraString = string.Join(string.Empty, paraArray);
            var bytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(paraString));

            var signStrBuilder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                signStrBuilder.Append($"{@byte:x2}");
            }

            return signStrBuilder.ToString().Equals(signature);
        }

        /// <summary>
        /// 确保校验请求参数有效。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timeStamp"></param>
        /// <param name="nonce"></param>
        /// <param name="signature"></param>
        public void Check(string token, string timeStamp, string nonce, string signature)
        {
            if (!Validate(token, timeStamp, nonce, signature))
            {
                throw new SignatureInvalidException();
            }
        }
        
        /// <summary>
        /// 确保数据签名有效。
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="sessionKey"></param>
        /// <param name="signature"></param>
        public void Check(string rawData, string sessionKey, string signature)
        {
            if (!Validate(rawData, sessionKey, signature))
            {
                throw new SignatureInvalidException();
            }
        }
    }
}