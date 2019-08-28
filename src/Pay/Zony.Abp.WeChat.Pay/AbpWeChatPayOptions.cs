namespace Zony.Abp.WeChat.Pay
{
    public class AbpWeChatPayOptions
    {
        /// <summary>
        /// 微信支付的 API 密钥。
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 支付回调地址，用于接收支付结果通知。
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 是否处于沙箱模式。
        /// </summary>
        public bool IsSandBox { get; set; }

        /// <summary>
        /// 绑定的支付应用 Id。
        /// </summary>
        public string AppId { get; set; }

        public AbpWeChatPayOptions()
        {
            IsSandBox = false;
        }
    }
}