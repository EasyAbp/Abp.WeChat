using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Zony.Abp.WeiXin.Pay.Models;

namespace Zony.Abp.WeiXin.Pay.Infrastructure
{
    public class SignatureGenerator : ISignatureGenerator, ISingletonDependency
    {
        private readonly AbpWeiXinPayOptions _abpWeiXinPayOptions;

        public SignatureGenerator(IOptions<AbpWeiXinPayOptions> abpWeiXinPayOptions)
        {
            _abpWeiXinPayOptions = abpWeiXinPayOptions.Value;
        }

        public string Generate(WeChatPayRequest payRequest)
        {
            var signStr = $"{payRequest.GetWaitForSignatureStr()}&key={_abpWeiXinPayOptions.ApiKey}";

            var signBytes = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(signStr));

            var sb = new StringBuilder();
            foreach (var @byte in signBytes)
            {
                sb.Append($"{@byte:x2}");
            }

            return sb.ToString().ToUpper();
        }
    }
}