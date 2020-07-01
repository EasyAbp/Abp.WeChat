using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public static class AesHelper
    {
        public static string AesDecrypt(string inputData, string iv, string key)
        {
            var encryptedData = Convert.FromBase64String(inputData.Replace(" ", "+"));
            
            var rijndaelCipher = new RijndaelManaged
            {
                Key = Convert.FromBase64String(key.Replace(" ", "+")),
                IV = Convert.FromBase64String(iv.Replace(" ", "+")),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            
            var transform = rijndaelCipher.CreateDecryptor();
            
            var plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            
            return Encoding.UTF8.GetString(plainText);
        }
    }
}