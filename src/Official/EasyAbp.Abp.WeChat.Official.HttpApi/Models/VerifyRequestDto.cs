namespace EasyAbp.Abp.WeChat.Official.HttpApi.Models
{
    public class VerifyRequestDto
    {
        /// <summary>
        /// 微信加密签名。
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳。
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数。
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 需要返回给微信公众平台的随机字符串。
        /// </summary>
        public string EchoStr { get; set; }
    }
}