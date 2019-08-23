namespace Zony.Abp.WeiXin.Pay
{
    public class AbpWeiXinPayOptions
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

        public AbpWeiXinPayOptions()
        {
            IsSandBox = false;
        }
    }
}