using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using EasyAbp.Abp.WeChat.Pay.Models;

namespace EasyAbp.Abp.WeChat.Pay.Services.Pay
{
    /// <summary>
    /// 集成了普通商户的微信支付 API 接口。
    /// </summary>
    public class OrdinaryMerchantPayService : WeChatPayService
    {
        protected readonly string UnifiedOrderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        protected readonly string RefundUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";
        protected readonly string OrderQueryUrl = "https://api.mch.weixin.qq.com/pay/orderquery";
        protected readonly string CloseOrderUrl = "https://api.mch.weixin.qq.com/pay/closeorder";
        protected readonly string RefundQueryUrl = "https://api.mch.weixin.qq.com/pay/refundquery";

        #region > 统一下单接口 <

        /// <summary>
        /// 除付款码支付场景以外，商户系统先调用该接口在微信支付服务后台生成预支付交易单，返回正确的预支付交易会话标识后再按 Native、JSAPI、APP 等不同场景生成交易串调起支付。
        /// </summary>
        /// <param name="appId">微信支付分配的唯一 Id。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="deviceInfo">终端设备号 (门店号或收银设备 Id)，注意：PC 网页或 JSAPI 支付请传 "WEB"。</param>
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
        /// <param name="openId">当 <paramref name="tradeType"/> 参数为 <see cref="TradeType.JsApi"/> 时，此参数必填。</param>
        /// <param name="receipt">传入 Y 时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效。</param>
        /// <param name="sceneInfo">该字段常用于线下活动时的场景信息上报，支持上报实际门店信息，商户也可以按需求自己上报相关信息。</param>
        /// <param name="isProfitSharing">该笔订单是否参与分账操作。</param>
        public async Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string deviceInfo, string body, string detail, string attach,
            string outTradeNo, string feeType, int totalFee, string billCreateIp, string timeStart, string timeExpire,
            string goodsTag, string notifyUrl, string tradeType, string productId, string limitPay, string openId,
            string receipt, string sceneInfo, bool isProfitSharing = false)
        {
            if (tradeType == TradeType.JsApi && string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException($"当交易类型为 JsApi 时，参数 {nameof(openId)} 必须传递有效值。");
            }

            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
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
            request.AddParameter("scene_info", sceneInfo);

            if (isProfitSharing)
            {
                request.AddParameter("profit_sharing", "Y");
            }
            
            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(UnifiedOrderUrl, request);
        }

        /// <summary>
        /// 除付款码支付场景以外，商户系统先调用该接口在微信支付服务后台生成预支付交易单，返回正确的预支付交易会话标识后再按 Native、JSAPI、APP 等不同场景生成交易串调起支付。
        /// </summary>
        /// <param name="appId">微信支付分配的唯一 Id。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="body">具体的商品描述信息，建议根据不同的场景传递不同的描述信息。</param>
        /// <param name="attach">附加数据，在查询 API 和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据。</param>
        /// <param name="outTradeNo">商户系统内部订单号，要求 32 个字符内，只能是数字、大小写字母_-|* 且在同一个商户号下唯一。</param>
        /// <param name="totalFee">订单总金额，只能为整数，单位是分。</param>
        /// <param name="tradeType">交易类型，请参考 <see cref="TradeType"/> 的定义。</param>
        /// <param name="openId">当 <paramref name="tradeType"/> 参数为 <see cref="TradeType.JsApi"/> 时，此参数必填。</param>
        /// <param name="isProfitSharing">该笔订单是否参与分账操作。</param>
        public async Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string body, string attach, string outTradeNo, int totalFee,
            string tradeType, string openId, bool isProfitSharing = false)
        {
            var options = await GetAbpWeChatPayOptions();

            return await UnifiedOrderAsync(appId, mchId, null, body, null, attach,
                outTradeNo, null, totalFee, "127.0.0.1", null, null,
                null, options.NotifyUrl, tradeType, null, null,
                openId, null, null, isProfitSharing);
        }

        #endregion

        #region > 申请退款接口 <

        /// <summary>
        /// 申请退款功能，支持针对指定订单进行退款操作。
        /// </summary>
        /// <remarks>
        /// 注意交易时间超过一年的订单无法提交退款，并且如果一笔退款失败后重新提交，请不要更换退款单号，请使用原商户退款单号。
        /// </remarks>
        /// <param name="appId">微信小程序或公众号的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="orderNo">商户订单号，该订单号是商户系统内部生成的唯一编号。</param>
        /// <param name="refundOrderNo">商户退款单号，该单号同 <paramref name="orderNo"/> 参数一样，都是通过商户系统自行生成。</param>
        /// <param name="orderTotalFee">原始订单的总金额，单位为分。</param>
        /// <param name="refundFee">本次需要进行退款的金额，单位为分。注意该金额不能大于原始订单的总金额。</param>
        /// <param name="refundDescription">退款说明，当订单退款金额小于 1 元时，不会在退款消息中体现。</param>
        /// <returns>请求的结果，会被转换为 <see cref="XmlDocument"/> 实例并返回。</returns>
        public async Task<XmlDocument> RefundAsync(string appId, string mchId, string orderNo, string refundOrderNo,
            int orderTotalFee, int refundFee,
            string refundDescription = null)
        {
            var options = await GetAbpWeChatPayOptions();
            
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("out_refund_no", refundOrderNo);
            request.AddParameter("out_trade_no", orderNo);
            request.AddParameter("total_fee", orderTotalFee);
            request.AddParameter("refund_fee", refundFee);
            request.AddParameter("notify_url", options.RefundNotifyUrl);
            request.AddParameter("refund_desc", refundDescription);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(RefundUrl, request);
        }

        #endregion

        #region > 查询订单接口 <

        /// <summary>
        /// 根据微信订单号或者商户订单号，查询订单的详细信息。如果两个参数都被填写，优先使用微信订单号进行查询。
        /// </summary>
        /// <remarks>
        /// 商户可以通过查询订单接口主动查询订单状态，完成下一步的业务逻辑。一般来说，都是因为商户系统没有收到支付通知，需要主动
        /// 查询订单的状态才会调用本方法进行查询。
        /// </remarks>
        /// <param name="appId">微信小程序或公众号的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="weChatOrderNo">微信订单号。</param>
        /// <param name="orderNo">商户订单号，该订单号是商户系统内部生成的唯一编号。</param>
        /// <returns>请求的结果，会被转换为 <see cref="XmlDocument"/> 实例并返回。</returns>
        /// <exception cref="ArgumentException">当微信订单号和商户订单号都为 null 时，会抛出本异常。</exception>
        public async Task<XmlDocument> OrderQueryAsync(string appId, string mchId, string weChatOrderNo = null, string orderNo = null)
        {
            if (string.IsNullOrEmpty(weChatOrderNo) && string.IsNullOrEmpty(orderNo))
            {
                throw new ArgumentException("微信订单号或商户订单号必须传递一个有效参数。");
            }

            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            if (!string.IsNullOrEmpty(weChatOrderNo))
            {
                request.AddParameter("transaction_id", weChatOrderNo);
            }
            else
            {
                request.AddParameter("out_trade_no", orderNo);
            }
            
            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(OrderQueryUrl, request);
        }

        #endregion

        #region > 关闭订单接口 <

        /// <summary>
        /// 根据商户订单号，关闭指定的订单。
        /// </summary>
        /// <remarks>
        /// 以下情况需要调用关单接口：商户订单支付失败需要生成新单号重新发起支付，要对原订单号调用关单，避免重复支付；系统下单后，
        /// 用户支付超时，系统退出不再受理，避免用户继续，请调用关单接口。订单生成后不能马上调用关单接口，最短调用时间间隔为 5 分钟。
        /// </remarks>
        /// <param name="appId">微信小程序或公众号的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="orderNo">商户订单号，该订单号是商户系统内部生成的唯一编号。</param>
        /// <returns>请求的结果，会被转换为 <see cref="XmlDocument"/> 实例并返回。</returns>
        public async Task<XmlDocument> CloseOrderAsync(string appId, string mchId, string orderNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            request.AddParameter("out_trade_no", orderNo);

            var options = await GetAbpWeChatPayOptions();

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), options.ApiKey);
            request.AddParameter("sign", signStr);

            return await RequestAndGetReturnValueAsync(CloseOrderUrl, request);
        }

        #endregion

        #region > 查询退款接口 <

        #endregion
    }
}