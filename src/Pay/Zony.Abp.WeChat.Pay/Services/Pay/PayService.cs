using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Zony.Abp.WeChat.Pay.Exceptions;
using Zony.Abp.WeChat.Pay.Models;

namespace Zony.Abp.WeChat.Pay.Services.Pay
{
    public class PayService : WeChatPayService
    {
        protected readonly string UnifiedOrderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        protected readonly string RefundUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";
        protected readonly string QueryOrderUrl = "https://api.mch.weixin.qq.com/pay/orderquery";
        protected readonly string CloseOrderUrl = "https://api.mch.weixin.qq.com/pay/closeorder";

        protected readonly AbpWeChatPayOptions AbpWeChatPayOptions;

        public PayService(IOptions<AbpWeChatPayOptions> aboWeChatPayOptions)
        {
            AbpWeChatPayOptions = aboWeChatPayOptions.Value;

            if (AbpWeChatPayOptions.IsSandBox)
            {
                UnifiedOrderUrl = " https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder";
                RefundUrl = "https://api.mch.weixin.qq.com/sandboxnew/secapi/pay/refund";
                QueryOrderUrl = "https://api.mch.weixin.qq.com/sandboxnew/pay/orderquery";
                CloseOrderUrl = "https://api.mch.weixin.qq.com/sandboxnew/pay/closeorder";
            }
        }

        /// <summary>
        /// 统一下单功能，支持除付款码支付场景以外的预支付交易单生成。生成后，根据返回的预支付交易会话标识，前端再根据不同的场景
        /// 唤起支付行为。
        /// </summary>
        /// <param name="appId">微信小程序或公众号的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="body">商品描述，例如 “腾讯充值中心 - QQ 会员充值” 。</param>
        /// <param name="orderNo">商户订单号，该订单号是商户系统内部生成的唯一编号。</param>
        /// <param name="totalFee">订单总金额，单位为分。</param>
        /// <param name="tradeType">本次的交易类型，具体参数可以参考 <see cref="TradeType"/> 的定义。</param>
        /// <param name="openId">用户标识，当 <paramref name="tradeType"/> 的类型为 <see cref="TradeType.JsApi"/> 时，本参数
        /// 必须传递。否则方法会抛出 <see cref="ArgumentException"/> 异常。</param>
        /// <param name="attach">附加参数，最大长度为 127，将来在微信支付进行回调通知时，会一并传递回来。</param>
        /// <returns>请求的结果，会被转换为 <see cref="XmlDocument"/> 实例并返回。</returns>
        public Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string body, string orderNo, int totalFee,
            string tradeType,
            string openId = null,
            string attach = null)
        {
            if (tradeType == TradeType.JsApi && string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException($"当交易类型为 JsApi 时，参数 {nameof(openId)} 必须传递有效值。");
            }

            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("body", body);
            request.AddParameter("out_trade_no", orderNo);
            request.AddParameter("total_fee", totalFee);
            request.AddParameter("spbill_create_ip", "127.0.0.1");
            request.AddParameter("notify_url", AbpWeChatPayOptions.NotifyUrl);
            request.AddParameter("openid", openId);
            request.AddParameter("attach", attach);
            request.AddParameter("trade_type", tradeType);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(UnifiedOrderUrl, request);
        }

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
        public Task<XmlDocument> RefundAsync(string appId, string mchId, string orderNo, string refundOrderNo,
            int orderTotalFee, int refundFee,
            string refundDescription = null)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("out_refund_no", refundOrderNo);
            request.AddParameter("out_trade_no", orderNo);
            request.AddParameter("total_fee", orderTotalFee);
            request.AddParameter("refund_fee", refundFee);
            request.AddParameter("notify_url", AbpWeChatPayOptions.RefundNotifyUrl);
            request.AddParameter("refund_desc", refundDescription);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(RefundUrl, request);
        }

        #region > 查询订单 <

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
        public Task<XmlDocument> QueryOrderAsync(string appId, string mchId, string weChatOrderNo = null, string orderNo = null)
        {
            if (string.IsNullOrEmpty(weChatOrderNo) && string.IsNullOrEmpty(orderNo))
            {
                throw new ArgumentException("微信订单号或商户订单号必须传递一个有效参数。");
            }

            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            if (string.IsNullOrEmpty(weChatOrderNo))
            {
                request.AddParameter("transaction_id", weChatOrderNo);
            }
            else
            {
                request.AddParameter("out_trade_no", orderNo);
            }

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(QueryOrderUrl, request);
        }

        #endregion

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
        public Task<XmlDocument> CloseOrderAsync(string appId, string mchId, string orderNo)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());

            request.AddParameter("out_trade_no", orderNo);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(CloseOrderUrl, request);
        }

        protected virtual async Task<XmlDocument> RequestAndGetReturnValueAsync(string targetUrl, WeChatPayParameters requestParameters)
        {
            var result = await WeChatPayApiRequester.RequestAsync(targetUrl, requestParameters.ToXmlStr());
            if (result.SelectSingleNode("/xml/err_code") != null ||
                result.SelectSingleNode("/xml/return_code")?.InnerText != "SUCCESS" ||
                result.SelectSingleNode("/xml/return_msg")?.InnerText != "OK")
            {
                var errMsg = $"微信支付接口调用失败，具体失败原因：{result.SelectSingleNode("/xml/err_code_des")?.InnerText ?? result.SelectSingleNode("/xml/return_msg")?.InnerText}";
                Logger.Log(LogLevel.Error, errMsg, targetUrl, requestParameters);

                var exception = new CallWeChatPayApiException(errMsg);
                exception.Data.Add(nameof(targetUrl),targetUrl);
                exception.Data.Add(nameof(requestParameters),requestParameters);
                
                throw exception;
            }

            return result;
        }
    }
}