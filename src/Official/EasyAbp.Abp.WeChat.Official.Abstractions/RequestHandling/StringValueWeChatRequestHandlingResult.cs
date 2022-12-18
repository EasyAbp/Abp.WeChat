using EasyAbp.Abp.WeChat.Common.RequestHandling;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public class StringValueWeChatRequestHandlingResult : WeChatRequestHandlingResult
{
    public string Value { get; set; }

    public StringValueWeChatRequestHandlingResult(bool success, string value, string failureReason = null)
    {
        Success = success;
        FailureReason = failureReason;
        Value = value;
    }
}