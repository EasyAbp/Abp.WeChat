using System.Security.Cryptography;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Zony.Abp.WeChat.Common.Infrastructure.Signature
{
    public class SignatureGenerator : ISignatureGenerator, ISingletonDependency
    {
        public string Generate(WeChatParameters parameters,string apiKey = null)
        {
            var signStr = $"{parameters.GetWaitForSignatureStr()}{(apiKey != null ? $"&key={apiKey}" : "")}";

            var signBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signStr));

            var sb = new StringBuilder();
            foreach (var @byte in signBytes)
            {
                sb.Append($"{@byte:x2}");
            }

            return sb.ToString().ToUpper();
        }
    }
}