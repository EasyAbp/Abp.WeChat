namespace EasyAbp.Abp.WeChat.Pay.Options
{
    /// <summary>
    /// 配置类定义了微信支付模块的相关配置参数。
    /// </summary>
    public class AbpWeChatPayOptions
    {
        /// <summary>
        /// 微信支付的商户号，商户号在调用微信支付 API 的时候需要使用。
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 微信支付的 API 密钥信息，会在后续进行签名时被使用。
        /// </summary>
        public string ApiKey { get; set; }

        #region > 可选参数 <

        /// <summary>
        /// 配置微信支付是否处于沙箱模式。当处于沙箱模式时，所有的支付服务将会调用沙箱支付接口，该参数值默认为 false。
        /// </summary>
        public bool IsSandBox { get; set; }

        /// <summary>
        /// 支付结果的回调地址，用于接收支付结果通知。
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 退款结果的回调地址，用于接收退款结果通知。
        /// </summary>
        public string RefundNotifyUrl { get; set; }

        /// <summary>
        /// PKCS 12 证书所在的 BLOB 容器名称，不指定时采用默认容器。
        /// </summary>
        public string CertificateBlobContainerName { get; set; }

        /// <summary>
        /// PKCS 12 证书的 BLOB 标识符。
        /// </summary>
        public string CertificateBlobName { get; set; }

        /// <summary>
        /// PKCS 12 证书的密码，默认为商户号(<see cref="MchId"/>)。
        /// </summary>
        public string CertificateSecret { get; set; }

        #endregion

        /// <summary>
        /// 构建一个新的 <see cref="AbpWeChatPayModule"/> 实例。
        /// </summary>
        public AbpWeChatPayOptions()
        {
            IsSandBox = false;
        }
    }
}