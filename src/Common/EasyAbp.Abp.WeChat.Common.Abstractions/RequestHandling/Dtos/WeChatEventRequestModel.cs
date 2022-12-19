using System;

namespace EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

[Serializable]
public class WeChatEventRequestModel
{
    public string PostData { get; set; }

    public string MsgSignature { get; set; }

    public string Timestamp { get; set; }

    public string Notice { get; set; }
}