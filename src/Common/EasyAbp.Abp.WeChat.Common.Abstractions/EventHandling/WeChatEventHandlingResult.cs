namespace EasyAbp.Abp.WeChat.Common.EventHandling;

public class WeChatEventHandlingResult
{
    public bool Success { get; set; }

    public string FailureReason { get; set; }

    public WeChatEventHandlingResult()
    {
    }

    public WeChatEventHandlingResult(bool success, string failureReason = null)
    {
        Success = success;
        FailureReason = failureReason;
    }
}