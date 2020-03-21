using System;

namespace EasyAbp.Abp.WeChat.Common.Extensions
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取当前时刻的 Unix 时间戳。
        /// </summary>
        public static long GetNowTimeStamp()
        {
            return (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}