using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace EasyAbp.Abp.WeChat.Common.Extensions
{
    public static class HashHelper
    {
        public static string ToMd5(this string str)
        {
            var md5Bytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            var stringBuilder = new StringBuilder();
            foreach (var @byte in md5Bytes)
            {
                stringBuilder.Append(@byte.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public static byte[] Sha256(this byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(bytes);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VerifySha256(this byte[] bytes, byte[] hash)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(bytes).SequenceEqual(hash);
            }
        }
    }
}