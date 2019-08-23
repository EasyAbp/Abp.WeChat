using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeChat.Pay.Models;

namespace Zony.Abp.WeChat.Pay.Infrastructure
{
    public class SignatureGenerator : ISignatureGenerator, ISingletonDependency
    {
        private readonly AbpWeChatPayOptions _abpWeChatPayOptions;

        public SignatureGenerator(IOptions<AbpWeChatPayOptions> abpWeChatPayOptions)
        {
            _abpWeChatPayOptions = abpWeChatPayOptions.Value;
        }

        public string Generate(WeChatPayRequest payRequest)
        {
            var signStr = $"{payRequest.GetWaitForSignatureStr()}&key={_abpWeChatPayOptions.ApiKey}";

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