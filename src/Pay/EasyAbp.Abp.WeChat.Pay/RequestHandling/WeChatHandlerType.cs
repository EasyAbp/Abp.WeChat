using System;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling
{
    [Flags]
    public enum WeChatHandlerType
    {
        Paid = 1,
        Refund = 2
    }
}