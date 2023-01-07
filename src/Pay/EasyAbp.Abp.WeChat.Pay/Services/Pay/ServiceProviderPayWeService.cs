using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using EasyAbp.Abp.WeChat.Pay.Models;
using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.Pay
{
    /// <summary>
    /// 集成了服务商的微信支付 API 接口。
    /// </summary>
    public class ServiceProviderPayWeService : WeChatPayServiceBase
    {
        public ServiceProviderPayWeService(AbpWeChatPayOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
        {
        }

        #region > 原始 URL 常量定义 <

        protected const string UnifiedOrderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        protected const string RefundUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";
        protected const string OrderQueryUrl = "https://api.mch.weixin.qq.com/pay/orderquery";
        protected const string CloseOrderUrl = "https://api.mch.weixin.qq.com/pay/closeorder";
        protected const string RefundQueryUrl = "https://api.mch.weixin.qq.com/pay/refundquery";

        #endregion

        /// <summary>
        /// 除付款码支付场景以外，商户系统先调用该接口在微信支付服务后台生成预支付交易单，返回正确的预支付交易会话标识后再按不同场景生成交易串调起支付。
        /// </summary>
        /// <param name="appId">服务商商户的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id。<br/>如需在支付完成后获取 <paramref name="subOpenId"/> 则此参数必传。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="deviceInfo">终端设备号 (门店号或收银设备 Id)，注意：PC 网页或 JSAPI 支付请传 "WEB"。</param>
        /// <param name="receipt">传入 Y 时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效。</param>
        /// <param name="body">具体的商品描述信息，建议根据不同的场景传递不同的描述信息。</param>
        /// <param name="detail">商品详细描述，对于使用单品优惠的商户，该字段必须按照规范上传。</param>
        /// <param name="attach">附加数据，在查询 API 和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据。</param>
        /// <param name="outTradeNo">商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|* 且在同一个商户号下唯一。</param>
        /// <param name="feeType">符合 ISO 4217 标准的三位字母代码，默认人民币：CNY。</param>
        /// <param name="totalFee">订单总金额，只能为整数，单位是分。</param>
        /// <param name="billCreateIp">调用微信支付 API 的机器 IP，可以使用 IPv4 或 IPv6。</param>
        /// <param name="timeStart">订单生成时间，格式为 yyyyMMddHHmmss。</param>
        /// <param name="timeExpire">订单失效时间，格式为 yyyyMMddHHmmss。</param>
        /// <param name="goodsTag">订单优惠标记，代金券或立减优惠功能的参数。</param>
        /// <param name="notifyUrl">接收微信支付异步通知回调地址，通知 Url 必须为直接可访问的 Url，不能携带参数。</param>
        /// <param name="tradeType">交易类型，请参考 <see cref="TradeType"/> 的定义。</param>
        /// <param name="productId">当 <paramref name="tradeType"/> 参数为 <see cref="TradeType.Native"/> 时，此参数必填。</param>
        /// <param name="limitPay">指定支付方式，传递 no_credit 则说明不能使用信用卡支付。</param>
        /// <param name="openId">当 <paramref name="tradeType"/> 参数为 <see cref="TradeType.JsApi"/> 时，此参数必填。如果选择传 <paramref name="subOpenId"/>, 则必须传 <paramref name="subAppId"/>。</param>
        /// <param name="subOpenId">当 <paramref name="tradeType"/> 参数为 <see cref="TradeType.JsApi"/> 时，此参数必填。如果选择传 <paramref name="subOpenId"/>, 则必须传 <paramref name="subAppId"/>。</param>
        /// <param name="sceneInfo">该字段常用于线下活动时的场景信息上报，支持上报实际门店信息，商户也可以按需求自己上报相关信息。</param>
        public virtual async Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string subAppId,
            string subMchId, string deviceInfo, string receipt,
            string body, string detail, string attach, string outTradeNo, string feeType, int totalFee,
            string billCreateIp, string timeStart, string timeExpire, string goodsTag, string notifyUrl,
            string tradeType, string productId,
            string limitPay, string openId, string subOpenId, string sceneInfo)
        {
            if (tradeType == TradeType.JsApi && string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException($"当交易类型为 JsApi 时，参数 {nameof(openId)} 必须传递有效值。");
            }

            if (!string.IsNullOrEmpty(subOpenId) && string.IsNullOrEmpty(subAppId))
            {
                throw new ArgumentException($"传递子商户的 OpenId 时，参数 {nameof(subAppId)} 必须传递有效值。");
            }

            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_appid", subAppId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("device_info", deviceInfo);
            request.AddParameter("receipt", receipt);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("body", body);
            request.AddParameter("detail", detail);
            request.AddParameter("attach", attach);
            request.AddParameter("out_trade_no", outTradeNo);
            request.AddParameter("fee_type", feeType);
            request.AddParameter("total_fee", totalFee);
            request.AddParameter("spbill_create_ip", billCreateIp);
            request.AddParameter("time_start", timeStart);
            request.AddParameter("time_expire", timeExpire);
            request.AddParameter("goods_tag", goodsTag);
            request.AddParameter("notify_url", notifyUrl);
            request.AddParameter("trade_type", tradeType);
            request.AddParameter("product_id", productId);
            request.AddParameter("limit_pay", limitPay);
            request.AddParameter("openid", openId);
            request.AddParameter("sub_openid", subOpenId);
            request.AddParameter("scene_info", sceneInfo);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), Options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(UnifiedOrderUrl, request, mchId);
        }

        /// <summary>
        /// 当交易发生之后一段时间内，由于买家或者卖家的原因需要退款时，卖家可以通过退款接口将支付款退还给买家，微信支付将在收到退款请求并且验证成功之后，按照退款规则将
        /// 支付款按原路退到买家帐号上。
        /// </summary>
        /// <remarks>
        /// 注意：<br/>
        /// 1. 交易时间超过一年的订单无法提交退款。<br/>
        /// 2. 微信支付退款支持单笔交易分多次退款，多次退款需要提交原支付订单的商户订单号和设置不同的退款单号。申请退款总金额不能超过订单金额。 一笔退款失败后重新提交，请
        /// 不要更换退款单号，请使用原商户退款单号。<br/>
        /// 3. 请求频率限制：150 QPS，即每秒钟正常的申请退款请求次数不超过 150 次。<br/>
        /// 错误或无效请求频率限制：6 QPS，即每秒钟异常或错误的退款申请请求不超过 6 次。<br/>
        /// 4. 每个支付订单的部分退款次数不能超过 50 次。
        /// </remarks>
        /// <param name="appId">服务商商户的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="transactionId">微信生成的订单号，在支付通知中有返回。</param>
        /// <param name="outTradeNo">
        /// 商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。<br/>
        /// <paramref name="transactionId"/> 与 <paramref name="outTradeNo"/> 二选一，如果同时存在，优先级是：<paramref name="transactionId"/> 大于 <paramref name="outTradeNo"/>。
        /// </param>
        /// <param name="outRefundNo">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。</param>
        /// <param name="totalFee">订单总金额，单位为分，只能为整数。</param>
        /// <param name="refundFee">退款总金额，单位为分，只能为整数，可部分退款。</param>
        /// <param name="refundFeeType">退款货币类型，需与支付一致，或者不填。</param>
        /// <param name="refundDesc">若商户传入，会在下发给用户的退款消息中体现退款原因，当订单退款金额 ≤1 元并且属于部分退款，则不会在退款消息中体现退款原因。</param>
        /// <param name="refundAccount">仅针对老资金流商户使用，具体参考 <see cref="RefundAccountType"/> 的定义。</param>
        /// <param name="notifyUrl">异步接收微信支付退款结果通知的回调地址，通知 Url 必须为外网可访问的 Url，不允许带参数。如果传递了该参数，则商户平台上配置的回调地址将不会生效。</param>
        public virtual async Task<XmlDocument> RefundAsync(string appId, string mchId, string subAppId, string subMchId,
            string transactionId, string outTradeNo, string outRefundNo, int totalFee, int refundFee,
            string refundFeeType, string refundDesc, string refundAccount, string notifyUrl)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_appid", subAppId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_trade_no", outTradeNo);
            request.AddParameter("out_refund_no", outRefundNo);
            request.AddParameter("total_fee", totalFee);
            request.AddParameter("refund_fee", refundFee);
            request.AddParameter("refund_fee_type", refundFeeType);
            request.AddParameter("refund_desc", refundDesc);
            request.AddParameter("refund_account", refundAccount);
            request.AddParameter("notify_url", notifyUrl);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), Options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(RefundUrl, request, mchId);
        }

        /// <summary>
        /// 该接口提供所有微信支付订单的查询，商户可以通过该接口主动查询订单状态，完成下一步的业务逻辑。<br/>
        /// 需要调用查询接口的情况：<br/>
        /// 1. 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知。<br/>
        /// 2. 调用支付接口后，返回系统错误或未知交易状态情况。<br/>
        /// 3. 调用被扫支付 API，返回 USERPAYING 的状态。<br/>
        /// 4. 调用关单或撤销接口 API 之前，需确认支付状态。
        /// </summary>
        /// <param name="appId">服务商商户的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="transactionId">微信生成的订单号，在支付通知中有返回。</param>
        /// <param name="outTradeNo">
        /// 商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。<br/>
        /// <paramref name="transactionId"/> 与 <paramref name="outTradeNo"/> 二选一，如果同时存在，优先级是：<paramref name="transactionId"/> 大于 <paramref name="outTradeNo"/>。
        /// </param>
        public virtual async Task<XmlDocument> OrderQueryAsync(string appId, string mchId, string subAppId,
            string subMchId, string transactionId, string outTradeNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_appid", subAppId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_trade_no", outTradeNo);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), Options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(OrderQueryUrl, request, mchId);
        }

        /// <summary>
        /// 以下情况需要调用关单接口：<br/>
        /// 1. 商户订单支付失败需要生成新单号重新发起支付，要对原订单号调用关单，避免重复支付。<br/>
        /// 2. 系统下单后，用户支付超时，系统退出不再受理，避免用户继续，请调用关单接口。<br/>
        /// </summary>
        /// <param name="appId">服务商商户的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="outTradeNo">商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。</param>
        /// <returns></returns>
        public virtual async Task<XmlDocument> CloseOrderAsync(string appId, string mchId, string subAppId,
            string subMchId, string outTradeNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_appid", subAppId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("out_trade_no", outTradeNo);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), Options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(CloseOrderUrl, request, mchId);
        }

        /// <summary>
        /// 提交退款申请后，通过调用该接口查询退款状态。退款有一定延时，用零钱支付的退款 20 分钟内到账，银行卡支付的退款 3 个工作日后重新查询退款状态。
        /// </summary>
        /// <remarks>
        /// 注意：如果单个支付订单部分退款次数超过 20 次请使用退款单号查询。<br/>
        /// 当一个订单部分退款超过 10 笔后，商户用微信订单号或商户订单号调退款查询 API 查询退款时，默认返回前 10 笔和 total_refund_count（退款单总笔数）。<br/>
        /// 商户需要查询同一订单下超过 10 笔的退款单时，可传入订单号及 offset 来查询，微信支付会返回 offset 及后面的 10 笔，以此类推。<br/>
        /// 当商户传入的 offset 超过 total_refund_count，则系统会返回报错 PARAM_ERROR。
        /// </remarks>
        /// <param name="appId">服务商商户的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="subAppId">微信分配的子商户公众账号 Id。</param>
        /// <param name="subMchId">微信支付分配的子商户号。</param>
        /// <param name="transactionId">微信订单号。</param>
        /// <param name="outTradeNo">商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。</param>
        /// <param name="outRefundNo">商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔。</param>
        /// <param name="refundId">微信退款单号。</param>
        /// <param name="offset">偏移量，当部分退款次数超过 10 次时可使用，表示返回的查询结果从这个偏移量开始取记录。</param>
        public virtual async Task<XmlDocument> RefundQueryAsync(string appId, string mchId, string subAppId,
            string subMchId, string transactionId, string outTradeNo, string outRefundNo, string refundId, int offset)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("sub_appid", subAppId);
            request.AddParameter("sub_mch_id", subMchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("transaction_id", transactionId);
            request.AddParameter("out_trade_no", outTradeNo);
            request.AddParameter("out_refund_no", outRefundNo);
            request.AddParameter("refund_id", refundId);
            request.AddParameter("out_trade_no", offset);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), Options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(RefundQueryUrl, request, mchId);
        }
    }
}