using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
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
    }
}