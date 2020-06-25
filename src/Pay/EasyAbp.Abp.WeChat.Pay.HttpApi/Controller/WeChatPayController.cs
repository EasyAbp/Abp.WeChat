using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.Infrastructure;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.Pay.HttpApi.Controller
{
    [RemoteService]
    [ControllerName("WeChatPay")]
    [Route("/WeChatPay")]
    public class WeChatPayController : AbpController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly IWeChatPayOptionsResolver _optionsResolver;

        public WeChatPayController(IHttpContextAccessor httpContextAccessor,
            ISignatureGenerator signatureGenerator,
            IWeChatPayOptionsResolver optionsResolver)
        {
            _httpContextAccessor = httpContextAccessor;
            _signatureGenerator = signatureGenerator;
            _optionsResolver = optionsResolver;
        }

        /// <summary>
        /// 微信支付模块提供的支付成功通知接口，开发人员需要实现 <see cref="IWeChatPayHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("Notify")]
        public virtual async Task<ActionResult> Notify()
        {
            var handlers = ServiceProvider.GetServices<IWeChatPayHandler>();

            Request.EnableBuffering();
            using (var streamReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body))
            {
                var result = await streamReader.ReadToEndAsync();

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(result);
                var context = new WeChatPayHandlerContext
                {
                    WeChatRequestXmlData = xmlDocument
                };

                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(context);
                    if (!context.IsSuccess)
                    {
                        return BadRequest(BuildFailedXml(context.FailedResponse));
                    }
                }

                Request.Body.Position = 0;
            }

            return Ok(BuildSuccessXml());
        }

        /// <summary>
        /// 微信支付模块提供的退款回调接口，开发人员需要实现 <see cref="IWeChatPayRefundHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("RefundNotify")]
        public virtual async Task<ActionResult> RefundNotify()
        {
            var handlers = ServiceProvider.GetServices<IWeChatPayRefundHandler>();

            Request.EnableBuffering();
            using (var streamReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body))
            {
                var result = await streamReader.ReadToEndAsync();

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(result);
                var context = new WeChatPayHandlerContext
                {
                    WeChatRequestXmlData = xmlDocument
                };

                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(context);
                    if (!context.IsSuccess)
                    {
                        return BadRequest(BuildFailedXml(context.FailedResponse));
                    }
                }

                Request.Body.Position = 0;
            }

            return Ok(BuildSuccessXml());
        }

        /// <summary>
        /// 根据统一下单接口返回的预支付 Id 生成支付签名。
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="prepayId">预支付 Id。</param>
        [HttpGet]
        [Route("GetJsSdkWeChatPayParameters")]
        [ItemCanBeNull]
        public virtual async Task<ActionResult> GetJsSdkWeChatPayParameters([FromQuery] string appId, string prepayId)
        {
            if (string.IsNullOrEmpty(prepayId)) throw new UserFriendlyException("请传入有效的预支付订单 Id。");

            var nonceStr = RandomStringHelper.GetRandomString();
            var timeStamp = DateTimeHelper.GetNowTimeStamp();
            var package = $"prepay_id={prepayId}";
            var signType = "MD5";

            var option = await _optionsResolver.ResolveAsync();

            var @params = new WeChatParameters();
            @params.AddParameter("appId", appId);
            @params.AddParameter("nonceStr", nonceStr);
            @params.AddParameter("timeStamp", timeStamp);
            @params.AddParameter("package", package);
            @params.AddParameter("signType", signType);

            var paySignStr = _signatureGenerator.Generate(@params, MD5.Create(), option.ApiKey);

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
            return @"<xml>
                        <return_code><![CDATA[SUCCESS]]></return_code>
                        <return_msg><![CDATA[OK]]></return_msg>
                    </xml>";
        }

        private string BuildFailedXml(string failedReason)
        {
            return $@"<xml>
                        <return_code><![CDATA[FAIL]]></return_code>
                        <return_msg><![CDATA[{failedReason}]]></return_msg>
                    </xml>";
        }
    }
}