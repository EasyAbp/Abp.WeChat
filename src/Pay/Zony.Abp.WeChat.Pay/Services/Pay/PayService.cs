using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Zony.Abp.WeChat.Pay.Models;

namespace Zony.Abp.WeChat.Pay.Services.Pay
{
    public class PayService : WeChatPayService
    {
        private readonly string TargetUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        private readonly AbpWeChatPayOptions _abpWeChatPayOptions;

        public PayService(IOptions<AbpWeChatPayOptions> aboWeChatPayOptions)
        {
            _abpWeChatPayOptions = aboWeChatPayOptions.Value;

            if (_abpWeChatPayOptions.IsSandBox) TargetUrl = $" https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder";
        }

        /// <summary>
        /// 统一下单功能，支持除付款码支付场景以外的预支付交易单生成。
        /// </summary>
        public async Task<XmlDocument> UnifiedOrderAsync(string appId,string mchId,string body,string orderNo,int totalFee,string tradeType)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("appId",appId);
            request.AddParameter("mch_id",mchId);
            request.AddParameter("nonce_str",RandomHelper.GetRandom());
            request.AddParameter("body",body);
            request.AddParameter("out_trade_no",orderNo);
            request.AddParameter("total_fee",totalFee);
            request.AddParameter("spbill_create_ip","127.0.0.1");
            request.AddParameter("notify_url",_abpWeChatPayOptions.NotifyUrl);
            request.AddParameter("trade_type",tradeType);
            
            var signStr = SignatureGenerator.Generate(request,_abpWeChatPayOptions.ApiKey);
            request.AddParameter("sign",signStr);

            var result = await WeChatPayApiRequester.RequestAsync(TargetUrl, request.ToXmlStr());
            if (result.SelectSingleNode("/xml/err_code")?.InnerText != "SUCCESS") 
                throw new UserFriendlyException($"调用微信支付接口失败，具体信息：{result.InnerText}");

            return result;
        }
    }
}