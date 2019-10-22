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

        protected readonly AbpWeChatPayOptions AbpWeChatPayOptions;

        public PayService(IOptions<AbpWeChatPayOptions> aboWeChatPayOptions)
        {
            AbpWeChatPayOptions = aboWeChatPayOptions.Value;

            if (AbpWeChatPayOptions.IsSandBox)
            {
                UnifiedOrderUrl = " https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder";
                RefundUrl = "https://api.mch.weixin.qq.com/sandboxnew/secapi/pay/refund";
            }
        }

        /// <summary>
        /// 统一下单功能，支持除付款码支付场景以外的预支付交易单生成。
        /// </summary>
        public async Task<XmlDocument> UnifiedOrderAsync(string appId, string mchId, string body, string orderNo, int totalFee,
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

            var result = await WeChatPayApiRequester.RequestAsync(UnifiedOrderUrl, request.ToXmlStr());
            if (result.SelectSingleNode("/xml/err_code") != null ||
                result.SelectSingleNode("/xml/return_code")?.InnerText != "SUCCESS" ||
                result.SelectSingleNode("/xml/return_msg")?.InnerText != "OK")
            {
                throw new UserFriendlyException($"调用微信支付接口失败。");
            }

            return result;
        }

        /// <summary>
        /// 申请退款功能，支持针对指定订单进行退款操作。
        /// </summary>
        public async Task<XmlDocument> RefundAsync(string appId,string mchId,string orderNo,string refundOrderNo, int orderTotalFee,int refundFee,
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
            
            var result = await WeChatPayApiRequester.RequestAsync(RefundUrl, request.ToXmlStr());
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