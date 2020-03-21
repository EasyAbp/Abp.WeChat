using System;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.ProfitSharing
{
    /// <summary>
    /// 分账接收方参数的定义，用于每次进行分账操作时，传递的分账方列表。
    /// </summary>
    [Serializable]
    public class ProfitSharingReceiver
    {
        /// <summary>
        /// 分账接收方的类型，请参考 (<see cref="ProfitSharingReceiverType"/>)。
        /// </summary>
        [JsonProperty("type")] public string Type { get; private set; }

        /// <summary>
        /// 分账接收方的帐号，根据 <see cref="Type"/> 的不同，帐号的含义也不一样。
        /// </summary>
        [JsonProperty("account")] public string Account { get; private set; }

        /// <summary>
        /// 分账接收方的分账金额，单位是分。
        /// </summary>
        [JsonProperty("amount")] public int Amount { get; private set; }

        /// <summary>
        /// 分账的原因描述，分账账单中需要体现。
        /// </summary>
        [JsonProperty("description")] public string Description { get; private set; }

        protected ProfitSharingReceiver()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="ProfitSharingReceiver"/> 对象。
        /// </summary>
        /// <param name="type">分账接收方的类型，请参考 (<see cref="ProfitSharingReceiverType"/>)。</param>
        /// <param name="account">分账接收方的帐号，根据 <see cref="Type"/> 的不同，帐号的含义也不一样。</param>
        /// <param name="amount">分账接收方的分账金额，单位是分。</param>
        /// <param name="description">分账的原因描述，分账账单中需要体现。</param>
        /// <exception cref="ArgumentException">当分账金额为负数时，会导致本异常的抛出。</exception>
        public ProfitSharingReceiver(string type, string account, int amount, string description)
        {
            if (amount <= 0) throw new ArgumentException("请指定有效的分账金额。", nameof(account));

            Type = type;
            Account = account;
            Amount = amount;
            Description = description;
        }
    }
}