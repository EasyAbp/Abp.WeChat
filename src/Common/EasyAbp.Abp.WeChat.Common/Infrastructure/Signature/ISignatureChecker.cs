namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;

public interface ISignatureChecker
{
    /// <summary>
    /// 校验请求参数是否有效。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="timeStamp"></param>
    /// <param name="nonce"></param>
    /// <param name="signature"></param>
    /// <returns>返回 true 说明是有效请求，返回 false 说明是无效请求。</returns>
    bool Validate(string token, string timeStamp, string nonce, string signature);

    /// <summary>
    /// 检验数据签名是否有效。
    /// </summary>
    /// <param name="rawData"></param>
    /// <param name="sessionKey"></param>
    /// <param name="signature"></param>
    /// <returns>返回 true 说明是有效数据，返回 false 说明是无效数据。</returns>
    bool Validate(string rawData, string sessionKey, string signature);

    /// <summary>
    /// 确保校验请求参数有效。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="timeStamp"></param>
    /// <param name="nonce"></param>
    /// <param name="signature"></param>
    void Check(string token, string timeStamp, string nonce, string signature);

    /// <summary>
    /// 确保数据签名有效。
    /// </summary>
    /// <param name="rawData"></param>
    /// <param name="sessionKey"></param>
    /// <param name="signature"></param>
    void Check(string rawData, string sessionKey, string signature);
}