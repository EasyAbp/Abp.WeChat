using EasyAbp.Abp.WeChat.Common.RequestHandling;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public class GetJsSdkConfigParametersResult : WeChatRequestHandlingResult
{
    public string AppId { get; set; }

    public string NonceStr { get; set; }

    public long TimeStamp { get; set; }


    public string SignStr { get; set; }

    public string Ticket { get; set; }

    public GetJsSdkConfigParametersResult()
    {
    }

    public GetJsSdkConfigParametersResult(string appId, string nonceStr, long timeStamp, string signStr, string ticket)
    {
        Success = true;

        AppId = appId;
        NonceStr = nonceStr;
        TimeStamp = timeStamp;
        SignStr = signStr;
        Ticket = ticket;
    }

    public GetJsSdkConfigParametersResult(string failureReason = null)
    {
        Success = false;
        FailureReason = failureReason;
    }
}