using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.Infrastructure;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.Pay.HttpApi.Controller
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChatPay")]
    [Route("/wechat-pay")]
    public class WeChatPayController : AbpControllerBase
    {
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly IWeChatPayAsyncLocal _weChatPayAsyncLocal;
        private readonly IWeChatPayOptionsResolver _optionsResolver;

        public WeChatPayController(
            ISignatureGenerator signatureGenerator,
            IWeChatPayAsyncLocal weChatPayAsyncLocal,
            IWeChatPayOptionsResolver optionsResolver)
        {
            _signatureGenerator = signatureGenerator;
            _weChatPayAsyncLocal = weChatPayAsyncLocal;
            _optionsResolver = optionsResolver;
        }

        /// <summary>
        /// 微信支付模块提供的支付成功通知接口，开发人员需要实现 <see cref="IWeChatPayHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("notify")]
        [Route("notify/tenant-id/{tenantId}")]
        [Route("notify/app-id/{appId}")]
        [Route("notify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<ActionResult> Notify([CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            using var changeOptions = _weChatPayAsyncLocal.Change(options);

            var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayHandler>>()
                .Where(h => h.Type == WeChatHandlerType.Normal);

            Request.EnableBuffering();
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
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
        /// 微信支付模块提供的退款回调接口，开发人员需要实现 <see cref="IWeChatPayHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("refund-notify")]
        [Route("refund-notify/tenant-id/{tenantId}")]
        [Route("refund-notify/app-id/{appId}")]
        [Route("refund-notify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<ActionResult> RefundNotify([CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            using var changeOptions = _weChatPayAsyncLocal.Change(options);

            var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatPayHandler>>()
                .Where(h => h.Type == WeChatHandlerType.Refund);

            Request.EnableBuffering();
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
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
        [Route("js-sdk-config-parameters")]
        [ItemCanBeNull]
        public virtual async Task<ActionResult> GetJsSdkWeChatPayParameters([FromQuery] string appId, string prepayId)
        {
            if (string.IsNullOrEmpty(prepayId)) throw new UserFriendlyException("请传入有效的预支付订单 Id。");

            // 如果指定了 appId，请务必实现 IHttpApiWeChatOfficialOptionsProvider
            var options = await ResolveOptionsAsync(appId);

            using var changeOptions = _weChatPayAsyncLocal.Change(options);

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

        protected virtual async Task<IWeChatPayOptions> ResolveOptionsAsync(string appId)
        {
            var provider = LazyServiceProvider.LazyGetRequiredService<IHttpApiWeChatPayOptionsProvider>();
            return appId.IsNullOrWhiteSpace() ? await _optionsResolver.ResolveAsync() : await provider.GetAsync(appId);
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