namespace EasyAbp.Abp.WeChat.Common.EventHandling;

public class WeChatEventHandlingResult
{
    public bool Success { get; set; }

    public WeChatEventHandlingResult()
    {
    }

    public WeChatEventHandlingResult(bool success)
    {
        Success = success;
    }
}