using System.Security.Cryptography;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Signature
{
    public interface ISignatureGenerator
    {
        /// <summary>
        /// 根据传入的参数字典，生成签名数据。
        /// </summary>
        /// <returns>生成的签名数据。</returns>
        string Generate(WeChatParameters payRequest, HashAlgorithm hashAlgorithm, string apiKey = null);
    }
}