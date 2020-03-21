using System;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.ProfitSharing
{
    /// <summary>
    /// 分账接收方的基本定义，主要用于添加/删除分账接收方接口。
    /// </summary>
    [Serializable]
    public class ProfitSharingReceiverDefinition
    {
        /// <summary>
        /// 分账接收方的类型，请参考 (<see cref="ProfitSharingReceiverType"/>)。
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// 分账接收方的帐号，根据 <see cref="Type"/> 的不同，帐号的含义也不一样。
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; private set; }

        /// <summary>
        /// 分账接收方与特约商户的关系，请参考 (<see cref="ProfitSharingReceiverRelationType"/>)。
        /// </summary>
        [JsonProperty("relation_type")]
        public string RelationType { get; private set; }

        /// <summary>
        /// 分账接收方全称，当接收方类型为 <see cref="ProfitSharingReceiverType.MerchantId"/> 和 <see cref="ProfitSharingReceiverType.PersonalWeChatId"/> 的时候，本参数是必填的。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 自定义的分账关系，当 <see cref="RelationType"/> 的值为 <see cref="ProfitSharingReceiverRelationType.Custom"/> 时，本参数是必填的。
        /// </summary>
        [JsonProperty("custom_relation")]
        public string CustomRelation { get; set; }

        protected ProfitSharingReceiverDefinition()
        {
        }

        /// <summary>
        /// 构建一个新的 <see cref="ProfitSharingReceiverDefinition"/> 对象。
        /// </summary>
        /// <param name="type">分账接收方的类型，请参考 (<see cref="ProfitSharingReceiverType"/>)。</param>
        /// <param name="account">分账接收方的帐号，根据 <see cref="Type"/> 的不同，帐号的含义也不一样。</param>
        /// <param name="relationType">分账接收方与特约商户的关系，请参考 (<see cref="ProfitSharingReceiverRelationType"/>)。</param>
        /// <exception cref="ArgumentException">当缺少必填参数时，会抛出本异常。</exception>
        public ProfitSharingReceiverDefinition(string type, string account, string relationType)
        {
            Type = type;
            Account = account;
            RelationType = relationType;

            if (RelationType == ProfitSharingReceiverRelationType.Custom && string.IsNullOrEmpty(CustomRelation))
            {
                throw new ArgumentException("当分账方关系为 CUSTOM 时，必须传递自定义的分账关系。", nameof(CustomRelation));
            }

            if ((Type == ProfitSharingReceiverType.MerchantId || Type == ProfitSharingReceiverType.PersonalWeChatId) && string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("当接收方类型为商户或个人微信号时，分账接收方的全程属于必填参数。",nameof(Name));
            }
        }
    }
}