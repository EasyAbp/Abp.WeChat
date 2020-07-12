using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.Handlers
{
    public class RefundResolveHandler : IWeChatPayHandler
    {
        protected readonly IWeChatPayOptionsResolver WeChatPayOptionsResolver;

        public RefundResolveHandler(IWeChatPayOptionsResolver weChatPayOptionsResolver)
        {
            WeChatPayOptionsResolver = weChatPayOptionsResolver;
        }

        public WeChatHandlerType Type => WeChatHandlerType.Refund;

        public async Task HandleAsync(WeChatPayHandlerContext context)
        {
            var options = await WeChatPayOptionsResolver.ResolveAsync();
            var encryptedXml = context.WeChatRequestXmlData.SelectSingleNode("/xml/req_info")?.InnerText;
            var decryptXml = Decrypt(encryptedXml, options.ApiKey.ToMd5().ToLower());

            var root = context.WeChatRequestXmlData.DocumentElement?.SelectSingleNode("/xml/req_info");
            if (root != null)
            {
                root.InnerXml = decryptXml;
            }
        }

        public static string Decrypt(string decryptStr, string key)
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