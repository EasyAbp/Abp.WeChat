using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

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

        public static string AesGcmDecrypt(string apiV3Key, string associatedData, string nonce, string ciphertext)
        {
            var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            var aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(apiV3Key)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            var data = Convert.FromBase64String(ciphertext);
            var plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            var length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }
    }
}