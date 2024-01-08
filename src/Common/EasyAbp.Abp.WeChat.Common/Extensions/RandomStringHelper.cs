using System;
using System.Text;

namespace EasyAbp.Abp.WeChat.Common.Extensions
{
    public static class RandomStringHelper
    {
        public static string GetRandomString(int length = 30)
        {
            char[] constant =
            [
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            ];

            var sb = new StringBuilder();
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                sb.Append(constant[rd.Next(36)]);
            }

            return sb.ToString();
        }
    }
}