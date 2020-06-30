using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public static class AesHelper
    {
        public static byte[] AesEncrypt(byte[] data, byte[] iv, string strKey)
        {
            SymmetricAlgorithm des = Aes.Create();

            var byteArray = data;
            
            des.Key = Encoding.UTF8.GetBytes(strKey.PadRight(32));
            des.IV = iv;

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            
            cs.Write(byteArray, 0, byteArray.Length);
            cs.FlushFinalBlock();
            var cipherBytes = ms.ToArray();   
            
            return cipherBytes;
        }
        
        public static byte[] AesDecrypt(byte[] data, byte[] iv, string strKey)
        {
            SymmetricAlgorithm des = Aes.Create();

            des.Key = Encoding.UTF8.GetBytes(strKey.PadRight(32));
            des.IV = iv;

            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            using var originalMemory = new MemoryStream();
            
            var buffer = new byte[1024];
            int readBytes;
            
            while ((readBytes = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
                originalMemory.Write(buffer, 0, readBytes);
            }

            var decryptBytes = originalMemory.ToArray();

            return decryptBytes;
        }
    }
}