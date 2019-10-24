using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Options;
using Volo.Abp;
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
        /// 统一下单功能，支持除付款码支付场景以外的预支付交易单生成。
        /// </summary>
        public Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string body, string orderNo, int totalFee,
            string tradeType,
            string openId = null,
            string attach = null)
        {
           var request = new WeChatPayParameters();
            request.AddParameter("appid", appId);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("body", body);
            request.AddParameter("out_trade_no", orderNo);
            request.AddParameter("total_fee", totalFee);
            request.AddParameter("spbill_create_ip", "127.0.0.1");
            request.AddParameter("notify_url", AbpWeChatPayOptions.NotifyUrl);
            request.AddParameter("openid",openId);
            request.AddParameter("attach",attach);
            request.AddParameter("trade_type", tradeType);

            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(UnifiedOrderUrl, request);
        }

        /// <summary>
        /// 申请退款功能，支持针对指定订单进行退款操作。
        /// </summary>
        public Task<XmlDocument> RefundAsync(string appId,string mchId,string orderNo,string refundOrderNo, int orderTotalFee,int refundFee,
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
            request.AddParameter("refund_desc",refundDescription);
            
            var signStr = SignatureGenerator.Generate(request, MD5.Create(), AbpWeChatPayOptions.ApiKey);
            request.AddParameter("sign", signStr);

            return RequestAndGetReturnValueAsync(RefundUrl, request);
        }

        #region > 查询订单 <
        
        /// <summary>
        /// 根据微信订单号或者商户订单号，查询订单的详细信息。如果两个参数都被填写，优先使用微信订单号进行查询。
        /// </summary>
        /// <param name="appId">微信小程序或公众号的 AppId。</param>
        /// <param name="mchId">微信支付分配的商户号。</param>
        /// <param name="weChatOrderNo">微信订单号。</param>
        /// <param name="orderNo">商户订单号，该订单号是商户系统内部生成的唯一编号。</param>
        /// <returns>请求的结果，会被转换为 <see cref="XmlDocument"/> 实例并返回。</returns>
        /// <exception cref="ArgumentException">当微信订单号和商户订单号都为 null 时，会抛出本异常。</exception>
        public Task<XmlDocument> QueryOrderAsync(string appId, string mchId, string weChatOrderNo = null,string orderNo = null)
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
                request.AddParameter("transaction_id",weChatOrderNo);
            }
            else
            {
                request.AddParameter("out_trade_no",orderNo);
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
                throw new UserFriendlyException($"调用微信支付接口失败，具体失败原因：{result.SelectSingleNode("/xml/err_code_des")?.InnerText}");
            }

            return result;
        }
    }
}