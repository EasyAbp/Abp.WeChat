namespace EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

public class PlainTextResponseToWeChatModel : IResponseToWeChatModel
{
    public string Content { get; set; }
}