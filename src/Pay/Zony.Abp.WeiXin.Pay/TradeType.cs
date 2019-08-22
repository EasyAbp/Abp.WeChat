namespace Zony.Abp.WeiXin.Pay
{
    /// <summary>
    /// 微信支付方式的类型定义。
    /// </summary>
    public static class TradeType
    {
        /// <summary>
        /// JSAPI 支付（或者是微信小程序支付）。
        /// </summary>
        public const string JsApi = "JSAPI";

        /// <summary>
        /// Native 原生支付。
        /// </summary>
        public const string Native = "NATIVE";

        /// <summary>
        /// App 应用支付。
        /// </summary>
        public const string App = "APP";

        /// <summary>
        /// H5 网页支付。
        /// </summary>
        public const string H5 = "MWEB";
    }
}