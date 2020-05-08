using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Volo.Abp;
using EasyAbp.Abp.WeChat.Pay.Models;

namespace EasyAbp.Abp.WeChat.Pay.Services.ProfitSharing
{
    /// <summary>
    /// 境内服务商分账相关接口的实现。
    /// </summary>
    public class ProfitSharingService : WeChatPayService
    {
        protected readonly string ProfitSharingUrl = "https://api.mch.weixin.qq.com/secapi/pay/profitsharing";
        protected readonly string MultiProfitSharingUrl = "https://api.mch.weixin.qq.com/secapi/pay/multiprofitsharing";
        protected readonly string ProfitSharingQueryUrl = "https://api.mch.weixin.qq.com/pay/profitsharingquery";
        protected readonly string ProfitSharingAddReceiverUrl = "https://api.mch.weixin.qq.com/pay/profitsharingaddreceiver";
        protected readonly string ProfitSharingRemoveReceiverUrl = "https://api.mch.weixin.qq.com/pay/profitsharingremovereceiver";
        protected readonly string ProfitSharingFinishUrl = "https://api.mch.weixin.qq.com/secapi/pay/profitsharingfinish";
        protected readonly string ProfitSharingReturnUrl = "https://api.mch.weixin.qq.com/secapi/pay/profitsharingreturn";
        protected readonly string ProfitSharingReturnQueryUrl = "https://api.mch.weixin.qq.com/pay/profitsharingreturnquery";

        /// <summary>
        /// 单次分账请求按照传入的分账接收方账号和资金进行分账，同时会将订单剩余的待分账金额解冻给特约商户。故操作成功后，订单不能再进行分账，也不能进行分账完结。<br/><br/>
        /// 接口频率：30 QPS <br/>
        /// 是否需要证书：是
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="transactionId">微信支付订单号，不能传递商户系统自己生成的订单号。</param>
        /// <param name="outOrderNo">服务商系统内部的分账单号，在服务商系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），同一分账单号多次请求等同一次。</param>
        /// <param name="receivers">分账接收方列表，不超过 50 个接收对象。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id，可为空。</param>
        /// <exception cref="ArgumentException">当参数校验出现错误时，会抛出本异常。</exception>
        public async Task<XmlDocument> ProfitSharingAsync(string mchId, string subMchId, string appId, string transactionId, string outOrderNo,
            IList<ProfitSharingReceiver> receivers, string subAppId = null)
        {
            if (receivers.Count > 50)
            {
                throw new ArgumentException("分账接收方最大不能超过 50 个。", nameof(receivers));
            }

            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_order_no", outOrderNo);
            request.AddParameter("receivers", JsonConvert.SerializeObject(receivers));

            request.AddParameter("sub_appid", subAppId);

            var options = await GetAbpWeChatPayOptions();
            
            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingUrl, request);
        }

        /// <summary>
        /// 微信订单支付成功后，服务商代子商户发起分账请求，将结算后的钱分到分账接收方。多次分账请求仅会按照传入的分账接收方进行分账，不会对剩余的金额进行任何操作。
        /// 故操作成功后，在待分账金额不等于零时，订单依旧能够再次进行分账。<br/><br/>
        /// 接口频率：30 QPS <br/>
        /// 是否需要证书：是
        /// </summary>
        /// <remarks>
        /// 1. 对同一笔订单最多能发起 20 次多次分账请求。<br/>
        /// 2. 多次分账，可以将本商户作为分账接收方直接传入，实现释放资金给本商户的功能。
        /// </remarks>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="transactionId">微信支付订单号，不能传递商户系统自己生成的订单号。</param>
        /// <param name="outOrderNo">服务商系统内部的分账单号，在服务商系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），同一分账单号多次请求等同一次。</param>
        /// <param name="receivers">分账接收方列表，不超过 50 个接收对象。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id，可为空。</param>
        /// <exception cref="ArgumentException">当参数校验出现错误时，会抛出本异常。</exception>
        public async Task<XmlDocument> MultiProfitSharingAsync(string mchId, string subMchId, string appId, string transactionId, string outOrderNo,
            IList<ProfitSharingReceiver> receivers, string subAppId = null)
        {
            if (receivers.Count > 50)
            {
                throw new ArgumentException("分账接收方最大不能超过 50 个。", nameof(receivers));
            }

            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_order_no", outOrderNo);
            request.AddParameter("receivers", JsonConvert.SerializeObject(receivers));

            request.AddParameter("sub_appid", subAppId);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(MultiProfitSharingUrl, request);
        }

        /// <summary>
        /// 发起分账请求后，可调用此接口查询分账结果；发起分账完结请求后，可调用此接口查询分账完结的执行结果。<br/><br/>
        /// 接口频率：80 QPS <br/>
        /// 是否需要证书：否
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="transactionId">微信支付订单号。</param>
        /// <param name="outOrderNo">
        /// 查询分账结果，输入申请分账时的商户分账单号。 <br/>
        /// 查询分账完结的执行结果，输入发起分账完结时的商户分账单号。</param>
        public async Task<XmlDocument> ProfitSharingQueryAsync(string mchId, string subMchId, string transactionId, string outOrderNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_order_no", outOrderNo);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingQueryUrl, request);
        }

        /// <summary>
        /// 服务商代子商户发起添加分账接收方请求，后续可通过发起分账请求将结算后的钱分到该分账接收方。<br/><br/>
        /// 是否需要证书：否
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="newReceiver">需要新增的分账接收方对象。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id，可空。</param>
        public async Task<XmlDocument> ProfitSharingAddReceiverAsync(string mchId, string subMchId, string appId, ProfitSharingReceiverDefinition newReceiver, string subAppId = null)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("receiver", JsonConvert.SerializeObject(newReceiver));

            request.AddParameter("sub_appid", subAppId);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingAddReceiverUrl, request);
        }

        /// <summary>
        /// 服务商代子商户发起删除分账接收方请求，删除后不支持将结算后的钱分到该分账接收方。<br/><br/>
        /// 是否需要证书：否
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="receiverType">分账接收方类型，参考 (<see cref="ProfitSharingReceiverType"/>)。</param>
        /// <param name="receiverAccount">分账接收方帐号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id，可空。</param>
        public async Task<XmlDocument> ProfitSharingRemoveReceiverAsync(string mchId, string subMchId, string appId, string receiverType, string receiverAccount, string subAppId = null)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("receiver", JsonConvert.SerializeObject(new ProfitSharingReceiverDefinition(receiverType, receiverAccount, null)));

            request.AddParameter("sub_appid", subAppId);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingRemoveReceiverUrl, request);
        }

        /// <summary>
        /// 1. 不需要进行分账的订单，可直接调用本接口将订单的金额全部解冻给特约商户。<br/>
        /// 2. 调用多次分账接口后，需要解冻剩余资金时，调用本接口将剩余的分账金额全部解冻给特约商户。<br/>
        /// 3. 已调用请求单次分账后，剩余待分账金额为零，不需要再调用此接口。<br/><br/>
        /// 接口频率：30 QPS<br/> 
        /// 是否需要证书：是
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="transactionId">微信支付订单号，不能传递商户系统自己生成的订单号。</param>
        /// <param name="outOrderNo">服务商系统内部的分账单号，在服务商系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），同一分账单号多次请求等同一次。</param>
        /// <param name="description">分账完结的原因描述。</param>
        public async Task<XmlDocument> ProfitSharingFinishAsync(string mchId, string subMchId, string appId, string transactionId, string outOrderNo, string description)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_order_no", outOrderNo);
            request.AddParameter("description", description);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingFinishUrl, request);
        }

        /// <summary>
        /// 仅对订单进行退款时，如果订单已经分账，可以先调用此接口将指定的金额从分账接收方（仅限商户类型的分账接收方）回退给特约商户，然后再退款。<br/>
        /// 回退以原分账请求为依据，可以对分给分账接收方的金额进行多次回退，只要满足累计回退不超过该请求中分给接收方的金额。<br/>
        /// 此接口采用同步处理模式，即在接收到商户请求后，会实时返回处理结果。<br/><br/>
        /// 接口频率：30 QPS<br/>
        /// 是否需要证书：是
        /// </summary>
        /// <remarks>
        /// 此功能需要接收方在商户平台 - 交易中心 - 分账 - 分账接收设置下，开启同意分账回退后，才能使用。
        /// </remarks>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="outOrderNo">原发起分账请求时使用的商户后台系统的分账单号。</param>
        /// <param name="outReturnNo">此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一。</param>
        /// <param name="returnAccountType">回退方类型(<see cref="ProfitSharingReceiverType"/>)，暂时只支持从商户接收方回退分账金额。</param>
        /// <param name="returnAccount">回退方账号。</param>
        /// <param name="returnAmount">需要从分账接收方回退的金额，单位为分，只能为整数，不能超过原始分账单分出给该接收方的金额。</param>
        /// <param name="description">分账回退的原因描述。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id，可空。</param>
        public async Task<XmlDocument> ProfitSharingReturnAsync(string mchId, string subMchId, string appId, string outOrderNo, string outReturnNo, string returnAccountType,
            string returnAccount, int returnAmount, string description, string subAppId = null)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("out_order_no", outOrderNo);
            request.AddParameter("out_return_no", outReturnNo);
            request.AddParameter("return_account_type", returnAccountType);
            request.AddParameter("return_account", returnAccount);
            request.AddParameter("return_amount", returnAmount);
            request.AddParameter("description", description);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            request.AddParameter("sub_appid", subAppId);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingReturnUrl, request);
        }
        
        /// 商户需要核实回退结果，可调用此接口查询回退结果。<br/>
        /// 如果分账回退接口返回状态为处理中，可调用此接口查询回退结果。<br/><br/>
        /// 接口频率：30 QPS <br/>
        /// 是否需要证书：否
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="appId">微信分配的公众账号 Id。</param>
        /// <param name="outOrderNo">原发起分账请求时使用的商户后台系统的分账单号。</param>
        /// <param name="outReturnNo">此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一。</param>
        public async Task<XmlDocument> ProfitSharingReturnQueryAsync(string mchId, string subMchId, string appId, string outOrderNo, string outReturnNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("appid", appId);
            request.AddParameter("out_order_no", outOrderNo);
            request.AddParameter("out_return_no", outReturnNo);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(ProfitSharingReturnQueryUrl, request);
        }
    }
}