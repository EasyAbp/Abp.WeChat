using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.Pay.Controller
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChatPay")]
    [Route("/wechat-pay")]
    public class WeChatPayController : AbpControllerBase
    {
        private readonly IWeChatPayEventRequestHandlingService _eventRequestHandlingService;
        private readonly IWeChatPayClientRequestHandlingService _clientRequestHandlingService;

        public WeChatPayController(
            IWeChatPayEventRequestHandlingService eventRequestHandlingService,
            IWeChatPayClientRequestHandlingService clientRequestHandlingService)
        {
            _eventRequestHandlingService = eventRequestHandlingService;
            _clientRequestHandlingService = clientRequestHandlingService;
        }

        /// <summary>
        /// 微信支付模块提供的支付成功通知接口，开发人员需要实现 <see cref="IWeChatPayEventHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("notify")]
        [Route("notify/tenant-id/{tenantId}")]
        [Route("notify/mch-id/{mchId}")]
        [Route("notify/tenant-id/{tenantId}/mch-id/{mchId}")]
        public virtual async Task<ActionResult> NotifyAsync([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var result = await _eventRequestHandlingService.PaidNotifyAsync(new PaidNotifyInput
            {
                MchId = mchId,
                Xml = await GetPostDataAsync()
            });

            if (!result.Success)
            {
                return BadRequest(BuildFailedXml(result.FailureReason));
            }

            return Ok(BuildSuccessXml());
        }

        /// <summary>
        /// 微信支付模块提供的退款回调接口，开发人员需要实现 <see cref="IWeChatPayEventHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("refund-notify")]
        [Route("refund-notify/tenant-id/{tenantId}")]
        [Route("refund-notify/mch-id/{mchId}")]
        [Route("refund-notify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual async Task<ActionResult> RefundNotifyAsync([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var result = await _eventRequestHandlingService.RefundNotifyAsync(new RefundNotifyInput
            {
                MchId = mchId,
                Xml = await GetPostDataAsync()
            });

            if (!result.Success)
            {
                return BadRequest(BuildFailedXml(result.FailureReason));
            }

            return Ok(BuildSuccessXml());
        }

        /// <summary>
        /// 根据统一下单接口返回的预支付 Id 生成支付签名。
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="prepayId">预支付 Id。</param>
        /// <param name="tenantId">租户 Id</param>
        /// <param name="mchId">商户 Id</param>
        [HttpGet]
        [Route("js-sdk-config-parameters")]
        [Route("js-sdk-config-parameters/mch-id/{mchId}")]
        public virtual async Task<ActionResult> GetJsSdkWeChatPayParametersAsync(
            string mchId, [FromQuery] string appId, string prepayId)
        {
            var result = await _clientRequestHandlingService.GetJsSdkWeChatPayParametersAsync(
                new GetJsSdkWeChatPayParametersInput
                {
                    MchId = mchId,
                    AppId = appId,
                    PrepayId = prepayId
                });

            return new JsonResult(new
            {
                nonceStr = result.NonceStr,
                timeStamp = result.TimeStamp,
                package = result.Package,
                signType = result.SignType,
                paySign = result.PaySign
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

        protected virtual async Task<string> GetPostDataAsync()
        {
            using var streamReader = new StreamReader(HttpContext.Request.Body);

            var postData = await streamReader.ReadToEndAsync();

            Request.Body.Position = 0;

            return postData;
        }
    }
}