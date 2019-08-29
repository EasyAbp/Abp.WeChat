namespace Zony.Abp.WeChat.Pay
{
    /// <summary>
    /// 配置类定义了微信支付模块的相关配置参数。
    /// </summary>
    public class AbpWeChatPayOptions
    {
        /// <summary>
        /// 微信支付的 API 密钥信息，会在后续进行签名时被使用。
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 支付回调地址，用于接收支付结果通知。
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 配置微信支付是否处于沙箱模式。当处于沙箱模式时，所有的支付服务将会调用沙箱支付接口，该参数值默认为 false。
        /// </summary>
        public bool IsSandBox { get; set; }

        /// <summary>
        /// 发起微信支付请求的产品 App Id。如果是公众号需要发起支付请求则是公众号的 AppId，小程序则是小程序的 AppId。
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 构建一个新的 <see cref="AbpWeChatPayModule"/> 实例。
        /// </summary>
        public AbpWeChatPayOptions()
        {
            IsSandBox = false;
        }
    }
}