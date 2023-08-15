using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyAbp.Abp.WeChat.Pay.Security.Extensions
{
    public static class WeChatPaySecurityUtility
    {
        public static string Decrypt(string encryptedStr, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var dataBytes = Convert.FromBase64String(encryptedStr);
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