using EasyAbp.Abp.WeChat.Common.RequestHandling;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;

public class GetAccessTokenByCodeResult : WeChatRequestHandlingResult
{
    public int ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public string AccessToken { get; set; }

    public string Scope { get; set; }

    public int ExpiresIn { get; set; }

    public string OpenId { get; set; }

    public string RefreshToken { get; set; }

    public GetAccessTokenByCodeResult()
    {
    }

    public GetAccessTokenByCodeResult(
        string accessToken, string scope, int expiresIn, string openId, string refreshToken)
    {
        Success = true;

        AccessToken = accessToken;
        Scope = scope;
        ExpiresIn = expiresIn;
        OpenId = openId;
        RefreshToken = refreshToken;
    }

    public GetAccessTokenByCodeResult(int errorCode, string errorMessage)
    {
        Success = false;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        FailureReason = $"微信 Code2AccessToken 执行失败，错误信息：[{errorCode}] {errorMessage}";
    }
}