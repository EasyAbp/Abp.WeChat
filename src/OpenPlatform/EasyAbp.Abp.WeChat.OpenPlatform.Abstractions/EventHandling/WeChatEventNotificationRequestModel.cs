﻿namespace EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;

public class WeChatEventNotificationRequestModel
{
    public string PostData { get; set; }

    public string MsgSignature { get; set; }

    public string Timestamp { get; set; }

    public string Notice { get; set; }
}