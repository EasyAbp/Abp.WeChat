using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EasyAbp.Abp.WeChat.Common.Exceptions;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Signature
{
    public class SignatureChecker : ISignatureChecker, ITransientDependency
    {
        public bool Validate(string token, string timeStamp, string nonce, string signature)
        {
            var paraArray = new[] { token, timeStamp, nonce }.OrderBy(x => x).ToArray();
            var paraString = string.Join(string.Empty, paraArray);
            var bytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(paraString));

            var signStrBuilder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                signStrBuilder.Append($"{@byte:x2}");
            }

            return signStrBuilder.ToString().Equals(signature);
        }

        public bool Validate(string rawData, string sessionKey, string signature)
        {
            var paraArray = new[] { rawData, sessionKey }.ToArray();
            var paraString = string.Join(string.Empty, paraArray);
            var bytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(paraString));

            var signStrBuilder = new StringBuilder();

            foreach (var @byte in bytes)
            {
                signStrBuilder.Append($"{@byte:x2}");
            }

            return signStrBuilder.ToString().Equals(signature);
        }

        public void Check(string token, string timeStamp, string nonce, string signature)
        {
            if (!Validate(token, timeStamp, nonce, signature))
            {
                throw new SignatureInvalidException();
            }
        }

        public void Check(string rawData, string sessionKey, string signature)
        {
            if (!Validate(rawData, sessionKey, signature))
            {
                throw new SignatureInvalidException();
            }
        }
    }
}