﻿using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace EasyAbp.Abp.WeChat.Pay.Extensions
{
    public static class WeChatPayUtility
    {
        public static string Encrypt(this string src, byte[] key)
        {
            using var x509 = new X509Certificate2(key);
            using var rsa = (RSA)x509.PublicKey.Key;
            var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(src), RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(buff);
        }

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

        public static byte[] GetCertificate(string key, string associatedData, string nonce, string cipherText)
        {
            var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            var aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(key)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            var data = Convert.FromBase64String(cipherText);
            var plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            var length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return plaintext;
        }
    }
}