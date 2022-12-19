using System;
using EasyAbp.Abp.WeChat.Common.RequestHandling;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class GetJsSdkWeChatPayParametersResult : WeChatRequestHandlingResult
{
    public string NonceStr { get; set; }

    public long TimeStamp { get; set; }

    public string Package { get; set; }

    public string SignType { get; set; }

    public string PaySign { get; set; }

    public GetJsSdkWeChatPayParametersResult()
    {
    }

    public GetJsSdkWeChatPayParametersResult(
        string nonceStr, long timeStamp, string package, string signType, string paySign)
    {
        Success = true;

        NonceStr = nonceStr;
        TimeStamp = timeStamp;
        Package = package;
        SignType = signType;
        PaySign = paySign;
    }

    public GetJsSdkWeChatPayParametersResult(string failureReason = null)
    {
        Success = false;
        FailureReason = failureReason;
    }
}