using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Zony.Abp.WeChat.Pay.Extensions
{
    public static class WeChatPayToolUtility
    {
        public static string Encrypt(this string src, string pemPath)
        {
            var x509 = new X509Certificate2(pemPath);
            var rsa = (RSACryptoServiceProvider) x509.PublicKey.Key;
            var buffer = rsa.Encrypt(Encoding.UTF8.GetBytes(src), false);
            return Convert.ToBase64String(buffer);
        }
    }
}