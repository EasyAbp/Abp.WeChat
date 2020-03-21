namespace EasyAbp.Abp.WeChat.Pay.Services.ProfitSharing
{
    /// <summary>
    /// 分账接收方的类型定义。
    /// </summary>
    public static class ProfitSharingReceiverType
    {
        /// <summary>
        /// 接收方是商户，后续账户值请传递商户 Id。
        /// </summary>
        public const string MerchantId = "MERCHANT_ID";

        /// <summary>
        /// 接收方是个人，后续账户值请传递个人微信号。
        /// </summary>
        public const string PersonalWeChatId = "PERSONAL_WECHATID";

        /// <summary>
        /// 接收方是个人，后续账户值请传递该用户在父级 AppId 下的 OpenId。
        /// </summary>
        public const string PersonalOpenId = "PERSONAL_OPENID";

        /// <summary>
        /// 接收方是个人，后续账户值请传递该用户在子商户 AppId 下的 OpenId。
        /// </summary>
        public const string PersonalSubOpenId = "PERSONAL_SUB_OPENID";
    }
}