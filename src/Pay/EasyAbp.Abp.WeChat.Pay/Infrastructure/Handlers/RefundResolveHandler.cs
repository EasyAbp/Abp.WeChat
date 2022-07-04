using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.Handlers
{
    /// <summary>
    /// 退款处理器，用于处理微信支付退款逻辑。
    /// </summary>
    public class RefundResolveHandler : IWeChatPayHandler
    {
        protected readonly IWeChatPayOptionsResolver WeChatPayOptionsResolver;

        public WeChatHandlerType Type => WeChatHandlerType.Refund;

        public RefundResolveHandler(IWeChatPayOptionsResolver weChatPayOptionsResolver)
        {
            WeChatPayOptionsResolver = weChatPayOptionsResolver;
        }

        public virtual async Task HandleAsync(WeChatPayHandlerContext context)
        {
            var encryptedXml = context.WeChatRequestXmlData.SelectSingleNode("/xml/req_info")?.InnerText;
            if (encryptedXml == null)
            {
                return;
            }

            var options = await WeChatPayOptionsResolver.ResolveAsync();
            var decryptXml = Decrypt(encryptedXml, options.ApiKey.ToMd5().ToLower());

            var root = context.WeChatRequestXmlData.DocumentElement?.SelectSingleNode("/xml/req_info");
            if (root != null)
            {
                root.InnerXml = decryptXml;
            }
        }

        /// <summary>
        /// 对加密数据进行 AES 解码。
        /// </summary>
        /// <param name="decryptStr">已经被加密的字符串。</param>
        /// <param name="key">密钥。</param>
        /// <returns>完成 AES 解密的数据。</returns>
        protected virtual string Decrypt(string decryptStr, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var dataBytes = Convert.FromBase64String(decryptStr);
            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyBytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = rDel.CreateDecryptor();
            var resultBytes = cTransform.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
            return Encoding.UTF8.GetString(resultBytes);
        }
    }
}