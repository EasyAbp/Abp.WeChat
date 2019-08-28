using System.Security.Cryptography;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Zony.Abp.WeChat.Common.Extensions;
using Zony.Abp.WeChat.Common.Infrastructure;
using Zony.Abp.WeChat.Common.Infrastructure.Signature;
using Zony.Abp.WeChat.Pay.Infrastructure;

namespace Zony.Abp.WeChat.Pay.HttpApi.Controller
{
    [RemoteService]
    [ControllerName("WeChatPay")]
    [Route("/WeChatPay")]
    public class WeChatPayController : AbpController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly AbpWeChatPayOptions _abpWeChatPayOptions;

        public WeChatPayController(IHttpContextAccessor httpContextAccessor,
            ISignatureGenerator signatureGenerator,
            IOptions<AbpWeChatPayOptions> abpWeChatPayOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _signatureGenerator = signatureGenerator;
            _abpWeChatPayOptions = abpWeChatPayOptions.Value;
        }

        [HttpPost]
        [Route("Notify")]
        public virtual ActionResult Notify()
        {
            var handlers = ServiceProvider.GetServices<IWeChatPayHandler>();

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_httpContextAccessor.HttpContext.Request.Body);

            foreach (var handler in handlers)
            {
                handler.HandleAsync(xmlDocument);
            }
            
            return Ok(BuildSuccessXml());
        }
        
        /// <summary>
        /// 根据统一下单接口返回的预支付 Id 生成支付签名。
        /// </summary>
        /// <param name="prepayId">预支付 Id。</param>
        [HttpGet]
        [Route("GetJsSdkWeChatPayParameters")]
        public virtual ActionResult GetJsSdkWeChatPayParameters([FromQuery]string prepayId)
        {
            if(string.IsNullOrEmpty(prepayId)) throw new UserFriendlyException("请传入有效的预支付订单 Id。");
            
            var nonceStr = RandomHelper.GetRandom();
            var timeStamp = DateTimeHelper.GetNowTimeStamp();
            var package = $"prepay_id={prepayId}";
            var signType = "MD5";
            
            var @params = new WeChatParameters();
            @params.AddParameter("appId",_abpWeChatPayOptions.AppId);
            @params.AddParameter("nonceStr", nonceStr);
            @params.AddParameter("timeStamp", timeStamp);
            @params.AddParameter("package",package);
            @params.AddParameter("signType", signType);

            var paySignStr = _signatureGenerator.Generate(@params, MD5.Create(), _abpWeChatPayOptions.ApiKey);
            
            return new JsonResult(new
            {
                nonceStr,
                timeStamp,
                package,
                signType,
                paySign = paySignStr
            });
        }

        private string BuildSuccessXml()
        {
            return @"<xml><return_code><![CDATA[SUCCESS]]></return_code>
                      <return_msg><![CDATA[OK]]></return_msg>
                 </xml>";
        }
    }
}