namespace EasyAbp.Abp.WeChat.Common.RequestHandling;

public class WeChatEventRequestModel
{
    public string PostData { get; set; }

    public string MsgSignature { get; set; }

    public string Timestamp { get; set; }

    public string Notice { get; set; }
}