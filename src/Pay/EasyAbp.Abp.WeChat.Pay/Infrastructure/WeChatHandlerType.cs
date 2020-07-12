using System;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    [Flags]
    public enum WeChatHandlerType
    {
        Normal = 1,
        Refund = 2
    }
}