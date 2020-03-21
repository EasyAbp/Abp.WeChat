namespace EasyAbp.Abp.WeChat.Pay.Services.Pay
{
    /// <summary>
    /// 退款资金来源。
    /// </summary>
    public class RefundAccountType
    {
        /// <summary>
        /// 使用未结算资金退款（默认使用未结算资金退款）。
        /// </summary>
        public const string RefundSourceUnsettledFunds = "REFUND_SOURCE_UNSETTLED_FUNDS";

        /// <summary>
        /// 使用可用余额退款。
        /// </summary>
        public const string RefundSourceRechargeFunds = "REFUND_SOURCE_RECHARGE_FUNDS";
    }
}